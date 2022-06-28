using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Class used by actors to handle the gun they are currently holding
 *      Component used by actors.
 */
public class GunManager : MonoBehaviour
{
    #region VARIABLES
    public GameObject curGun;
    [SerializeField] private GameObject holder;
    [SerializeField] private Camera cam;
    public bool actorIsRunning=false; //boolean setup by the parent actor that will inform gunmanager wether or not player can ADS (if he run -> no)
    private UIManager uiManager;
    #endregion

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
        changeGun(curGun);
    }

    public GameObject getHolder() { return holder; }

    private void OnEnable()
    {
        curGun.transform.position = holder.transform.position;
    }

    //method that changes the player's gun
    public void changeGun(GameObject gun)
    {
        curGun = Instantiate(gun);
        curGun.transform.position = holder.transform.position;
        curGun.transform.parent = holder.transform;
        curGun.GetComponent<WeaponController>().setUiManager(uiManager); //to the newly-introduced gun, we give the UIManager
        uiManager.setMaxAmmo(curGun.GetComponent<WeaponController>().getMaxAmmo());
    }

    //The firing method, called by InputManager
    public void Fire()
    {
        curGun.GetComponent<WeaponController>().Fire();
    }

    public void Reload()
    {
        StartCoroutine(curGun.GetComponent<WeaponController>().Reload());
    }

    //method that aims down sight for the player
    public void AimDownSight(bool status)
    {
        if(status && !actorIsRunning)
        {
            Debug.Log("Aiming Down Sights !");
            holder.GetComponent<Animator>().SetBool("isADS", true); //the gunholder is the one possessing the animator
            StartCoroutine(uiManager.displayCrosshairs(true)); //we display the dot of the crosshairs
        }
        else if(!status)
        {
            Debug.Log("Not aiming !");
            holder.GetComponent<Animator>().SetBool("isADS", false);
            StartCoroutine(uiManager.displayCrosshairs(false));
        }
    }
}
