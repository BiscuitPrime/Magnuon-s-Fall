using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used by the Detonator prefab, that detonates a detonationTarget
 *  The detonator is created by the associated charge when placed and is useless afterwards
 */
public class DetonatorController : WeaponController
{
    private GameObject detonationTarget; //the target of the detonator

    public void setDetonationTarget(GameObject target)
    {
        detonationTarget = target;
    }

    //when fire is called, the detonator detonates its target if the target is active
    public override void Fire()
    {
        if(detonationTarget == null) //if no target available, the detonator just doesn't work
        {
            dud();
        }
        else if(detonationTarget.GetComponent<BreachChargeController>().isActive)
        {
            detonationTarget.GetComponent<BreachChargeController>().detonate(); //we tell the breach charge to explode
            Debug.Log("Fire in the hole !");
            isFiring = true;
            curAmmo--;
            uiManager.updateAmmoCounter(0);
        }
    }

    //when the detonator has expired its uses, it will fire a dud : nothing happens apart from a message
    private void dud()
    {
        Debug.Log("No target for detonation");
        isFiring = true;
        curAmmo--;
    }
}
