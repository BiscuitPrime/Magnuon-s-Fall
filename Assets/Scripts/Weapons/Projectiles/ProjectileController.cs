using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used by physical bullets/projectiles.
 *  This script is held by the projectiles themselves
 */
public class ProjectileController : MonoBehaviour
{
    private float dmg;

    public IEnumerator Life()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    //method handling the collision : it will collide with any actor and inflict damage
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Actors" && collision.gameObject.GetComponent<HealthModule>())
        {
            collision.gameObject.GetComponent<HealthModule>().Damage(dmg);
        }
    }

    //the bullet damage is set up by its parent weapon, since the projectile sccript can be applied to various different projectiles 
    public void setDamage(float damage)
    {
        dmg = damage;
    }
}
