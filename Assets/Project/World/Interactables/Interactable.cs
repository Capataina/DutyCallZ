using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactionDuration = 0;
    public bool isInteractable = true;
    public abstract void DisplayPrompt();
    public abstract void Interact(GameObject gameObject);
}
