using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Handles all the interactions the player can engage in.
*/
public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] float interactionRange;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] Camera cam;

    // the timer for holding down the interaction key
    float intTimer;

    private void Update()
    {
        // if the player is looking at an interactable within range
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactionRange, interactionLayer))
        {
            Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
            // if the object can be interacted with right now
            if (interactable.isInteractable)
            {
                // display the interaction prompt
                interactable.DisplayPrompt();
                // hold down the key until interaction triggers
                if (Input.GetKey(KeyCode.E))
                {
                    intTimer += Time.deltaTime;
                    if (intTimer >= interactable.interactionDuration)
                    {
                        interactable.Interact(gameObject);
                        intTimer = 0;
                    }
                }
                else
                {
                    intTimer = 0;
                }
                return;
            }
        }

        // if none of the above applies reset the interaction prompt
        UIManager.instance.ClearText();
    }
}
