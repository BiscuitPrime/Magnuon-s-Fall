using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Class used by breakable objects - like breakable walls 
 *      It requires a health module
 */
[RequireComponent(typeof(HealthModule))]
public class BreakableObjectController : MonoBehaviour
{
    #region VARIABLES
    private HealthModule healthModule;
    #endregion

    private void Awake()
    {
        healthModule = GetComponent<HealthModule>();
    }
}
