using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public IEnumerator Life()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
