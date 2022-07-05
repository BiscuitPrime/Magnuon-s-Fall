using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used for an inventory for the Player and other entities.
 *  It is merely the code, UI is not handled
 *  Component held by actors
 *  Actors can only carry two weapons
 */
public class InventoryModuleController : MonoBehaviour
{
    [SerializeField] private GameObject slot1;
    [SerializeField] private GameObject slot2;

    public void setItem(int weaponNumber,GameObject item)
    {
        if(weaponNumber==1)
        {
            slot1 = item;
        }
        else //to avoid errors, in case of a wrong weaponNumber, we equip it regardless to slot 2
        {
            slot2 = item;
        }
    }


}
