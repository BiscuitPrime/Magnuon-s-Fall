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
    [HideInInspector]public GameObject curGun=null;
    int slotIndicator=1; //indicates the current slot being used
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject slot2;
    private WeaponController curWeaponController;
    [SerializeField] private GameObject holder;
    [SerializeField] private Camera cam;
    public bool actorIsRunning=false; //boolean setup by the parent actor that will inform gunmanager wether or not player can ADS (if he run -> no)
    private UIManager uiManager;
    #endregion

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
        slot1 = Instantiate(slot1);
        slot1.transform.position = holder.transform.position;
        slot1.transform.parent = holder.transform;
        slot1.SetActive(false);
        slot2 = Instantiate(slot2);
        slot2.transform.position = holder.transform.position;
        slot2.transform.parent = holder.transform;
        slot2.SetActive(false);
        curGun = slot1;
        changeGun(1);
    }

    public GameObject getHolder() { return holder; }

    private void OnEnable()
    {
        curGun.transform.position = holder.transform.position;
    }

    //method that changes the player's gun
    public void changeGun(int slot)
    {
        curGun.SetActive(false);
        if(slot == 1)
        {
            curGun = slot1;
            slotIndicator = 1;
        }
        else
        {
            curGun = slot2;
            slotIndicator = 2;
        }
        curGun.SetActive(true);
        curGun.transform.position = holder.transform.position;
        curGun.transform.parent = holder.transform;
        if(curGun.GetComponent<WeaponController>() != null)
        {
            curWeaponController = curGun.GetComponent<WeaponController>();
        }
        curWeaponController.setUiManager(uiManager); //to the newly-introduced gun, we give the UIManager
        uiManager.setMaxAmmo(curWeaponController.getMaxAmmo());
    }

    //The firing method, called by InputManager
    public void Fire()
    {
        if (curGun.GetComponent<WeaponController>())
        {
            curWeaponController.Fire();
        }
    }

    public void Reload()
    {
        StartCoroutine(curWeaponController.Reload());
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

    public void Equip(int equipSlot)
    {
        changeGun(equipSlot);
    }

    public void addGun(GameObject gun)
    {
        if(slotIndicator==1)
        {
            slot1 = Instantiate(gun);
            slot1.SetActive(false);
            changeGun(1);
        }
        else
        {
            slot2 = Instantiate(gun);
            slot2.SetActive(false);
            changeGun(2);
        }
    }
}
