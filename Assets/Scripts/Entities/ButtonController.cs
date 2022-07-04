using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Script for the button that is meant to activate and desactivate entities like doors, traps, etc
 */
public class ButtonController : InteractableController
{
    #region VARIABLE
    [SerializeField] private GameObject connectedButtonObject;
    #endregion

    //the interaction for the button : we change the active status of the connected object
    public override void Interaction()
    {
        Debug.Log("Button pushed !");
        if(connectedButtonObject != null && connectedButtonObject.transform.GetComponent<Animator>() != null)
        {
            Animator anim = connectedButtonObject.transform.GetComponent<Animator>();
            anim.SetBool("isOpen", !anim.GetBool("isOpen"));
        }
    }
}
