using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float dmg;

    public IEnumerator Life()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Actors" && collision.gameObject.GetComponent<HealthModule>())
        {
            collision.gameObject.GetComponent<HealthModule>().Damage(dmg);
        }
    }
}
