using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *      Parent class of all Interactable objects
 */
public abstract class InteractableController : MonoBehaviour
{
    public void BaseInteract() { Interaction(); }
    public virtual void Interaction() { }
}
