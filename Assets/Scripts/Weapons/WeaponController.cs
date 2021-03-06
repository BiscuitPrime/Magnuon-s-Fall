using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used by the RAYCAST-CLASS guns to fire/perform their actions. Their methods are called by other scripts, such as GunManager.
 *  Component of the individual guns.
 */
public class WeaponController : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameObject muzzle;
    [SerializeField] private int maxAmmo=10;
    public int curAmmo;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float fireRate=10f;
    [SerializeField] private float damage = 3f;
    [SerializeField] private float range = 100f;
    protected bool isFiring; //REWORK INTO STATES
    private bool isReloading;
    [SerializeField] private bool IS_PROJECTILE = false;
    [SerializeField] private GameObject bullet;
    protected UIManager uiManager;
    #endregion

    private void Awake()
    {
        isFiring = false;
        isReloading = false;
        curAmmo = maxAmmo;
    }

    //fire method, called by GunManager
    public virtual void Fire()
    {
        if (!isFiring && !isReloading && curAmmo>0)  //as long as the gun isn't already firing AND it has ammo, we shoot a new bullet :
        {
            curAmmo --;
            uiManager.updateAmmoCounter(curAmmo);
            if(!IS_PROJECTILE)
            {
                //Raycast version :
                RaycastHit hit;
                if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, range)) //if the raycast hits anything
                {
                    //Additional note : I intend to have different results when firing on different things => hence why the two ifs are separated
                    if (hit.transform.tag == "Actors") //if the target hit is an actor 
                    {
                        hit.transform.GetComponent<HealthModule>().Damage(damage);
                    }
                }
            }
            else
            {
                // OBJECT VERSION :
               GameObject new_bullet = Instantiate(bullet);
               new_bullet.transform.position = muzzle.transform.position;
               new_bullet.transform.rotation = muzzle.transform.rotation;
               new_bullet.GetComponent<Rigidbody>().AddForce(new_bullet.transform.forward * 100 * 30f);
               ProjectileController projController = new_bullet.GetComponent<ProjectileController>();
               projController.setDamage(damage);
               StartCoroutine(projController.Life());
            }
            Debug.Log("Firing !");
            isFiring = true;
            StartCoroutine(firingRate());
        }
        if(curAmmo==0 && !isReloading) //if we attempt to fire with 0 ammo, we automatically reload
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    //method that artificially "slows" down the automatic fire, to not have immediate 36 pellet worth of shots
    private IEnumerator firingRate()
    {
        yield return new WaitForSeconds(fireRate/10);
        isFiring = false;
    }

    //reload method, called by GunManager
    public virtual IEnumerator Reload()
    {
        Debug.Log("Reloading !");
        yield return new WaitForSeconds(reloadTime);
        Debug.Log("Reloading complete !");
        curAmmo = maxAmmo;
        isReloading = false;
        yield return new WaitForEndOfFrame();
        uiManager.updateAmmoCounter(curAmmo);
    }

    public int getMaxAmmo()
    {
        return maxAmmo;
    }

    public void setUiManager(UIManager ui)
    {
        uiManager = ui;
        uiManager.updateAmmoCounter(curAmmo);
    }
}
