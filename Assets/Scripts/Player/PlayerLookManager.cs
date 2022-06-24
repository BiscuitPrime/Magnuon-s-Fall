using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Class handling the look mechanic of the player
 */
public class PlayerLookManager : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private Camera cam;
    private float upRotation = 0f;
    private readonly float xSensitivity = 40f;
    private readonly float ySensitivity = 40f;
    #endregion

    public void Look(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        //Looking up and down :
        upRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        upRotation = Mathf.Clamp(upRotation, -80f, 80f);
        //apply it :
        cam.transform.localRotation = Quaternion.Euler(upRotation, 0f, 0f);
        //rotate left-right :
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }   
}
