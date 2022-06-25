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

    public void changeGun(GameObject gun)
    {
        curGun = Instantiate(gun);
        curGun.transform.position = holder.transform.position;
        curGun.transform.parent = cam.transform;
    }

    //The firing method, called by InputManager
    public void Fire()
    {
        curGun.GetComponent<WeaponController>().Fire();
    }
}
