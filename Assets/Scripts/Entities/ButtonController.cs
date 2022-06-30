using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : InteractableController
{
    public override void Interaction()
    {
        Debug.Log("Button pushed !");
    }
}
