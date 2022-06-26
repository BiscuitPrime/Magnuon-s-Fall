using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used by the guns to fire/perform their actions. Their methods are called by other scripts, such as GunManager
 */
public class WeaponController : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private float fireRate=10f;
    private bool isFiring;
    #endregion

    private void Awake()
    {
        isFiring = false;
    }

    public void Fire()
    {
        if (!isFiring) 
        {
            GameObject new_bullet = Instantiate(bullet);
            new_bullet.transform.position = muzzle.transform.position;
            new_bullet.transform.rotation = muzzle.transform.rotation;
            new_bullet.GetComponent<Rigidbody>().AddForce(new_bullet.transform.forward * 100 * 30f);
            new_bullet.GetComponent<BulletController>().dmg = 3f;
            StartCoroutine(new_bullet.GetComponent<BulletController>().Life());
            Debug.Log("Firing !");
            isFiring = true;
            StartCoroutine(firingRate());
        }
    }

    private IEnumerator firingRate()
    {
        yield return new WaitForSeconds(fireRate/10);
        isFiring = false;
    }
}
