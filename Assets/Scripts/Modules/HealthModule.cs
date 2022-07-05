using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class used by the Health module, used by the players and enemies alike
 */
public class HealthModule : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private readonly float maxHP = 10f;
    public float curHP;
    #endregion

    private void Awake()
    {
        curHP = maxHP;
    }

    //Damage function, called when actor/entity is hit by the object hitting it
    public void Damage(float dmg)
    {
        curHP -= dmg;
        if (curHP <= 0)
        {
            //Should trigger actor/entity's death
            //For now, we simply destroy it.
            Destroy(gameObject);
        }
    }
}
