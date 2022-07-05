using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used for the Breach Charge, a placeable explosive charge
 */
public class BreachChargeController : WeaponController
{
    [SerializeField] private GameObject detonator;
    public bool isActive=false;

    //here, the fire() method called by GunManager doesn't "fire" but rather places the charge and changes the player's gun to the detonator
    public override void Fire()
    {
        GunManager gunManager = gameObject.transform.parent.parent.parent.GetComponent<GunManager>();
        gunManager.changeGun(detonator);
        gunManager.curGun.GetComponent<DetonatorController>().setDetonationTarget(gameObject); //we tell the detonator to target the charge
        gameObject.transform.parent = null;
        Debug.Log("Placed charge !");
        isFiring = true;
        StartCoroutine(activateChargeTimer());
    }

    private IEnumerator activateChargeTimer()
    {
        yield return new WaitForSeconds(1f);
        isActive = true;
    }

    //the detonate function that will tell the charge to explode
    //called by the detonator's Fire() function
    public void detonate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("target hit :" + hitCollider.transform.name);
            if(hitCollider.transform.tag=="BreakableObject")
            {
                hitCollider.transform.GetComponent<HealthModule>().Damage(100f);
            }
        }
        Destroy(gameObject);
    }
}
