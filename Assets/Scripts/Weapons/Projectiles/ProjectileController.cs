using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used by physical bullets/projectiles.
 */
public class ProjectileController : MonoBehaviour
{
    public float dmg;

    public IEnumerator Life()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    //method handling the collision : it will collide with any actor and inflict d
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Actors" && collision.gameObject.GetComponent<HealthModule>())
        {
            collision.gameObject.GetComponent<HealthModule>().Damage(dmg);
        }
    }

    public void setDamage(float damage)
    {
        dmg = damage;
    }
}
