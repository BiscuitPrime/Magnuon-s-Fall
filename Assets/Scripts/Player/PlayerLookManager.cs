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
    [SerializeField] private float interactRange = 60f;
    [SerializeField] private LayerMask interactMask;
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
    
    public void Interact()
    {
        //Debug.Log("Interacting !");
        RaycastHit hit;
        Debug.DrawRay(cam.transform.position, cam.transform.forward * interactRange, Color.black);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactRange, interactMask)) //if the raycast hits anything with the interactMask, the if triggers
        {
            if(hit.collider.GetComponent<InteractableController>() != null)
            {
                hit.collider.GetComponent<InteractableController>().BaseInteract();
            } 
        }
    }
}
