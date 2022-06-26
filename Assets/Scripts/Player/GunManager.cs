using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Class used by actors to handle the gun they are currently holding
 */
public class GunManager : MonoBehaviour
{
    #region VARIABLES
    public GameObject curGun;
    [SerializeField] private GameObject holder;
    [SerializeField] private Camera cam;
    #endregion

    private void Awake()
    {
        changeGun(curGun);
    }

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
    }

    //The firing method, called by InputManager
    public void Fire()
    {
        curGun.GetComponent<WeaponController>().Fire();
    }

    //method that aims down sight for the player
    public void AimDownSight(bool status)
    {
        if(status)
        {
            Debug.Log("Aiming Down Sights !");
            gameObject.GetComponentInChildren<Animator>().SetBool("isADS", true);
        }
        else
        {
            Debug.Log("Not aiming !");
            gameObject.GetComponentInChildren<Animator>().SetBool("isADS", false);
        }
    }
}
