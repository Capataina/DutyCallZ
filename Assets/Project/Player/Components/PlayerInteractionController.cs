using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] float interactionRange;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] Camera cam;

    float intTimer;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactionRange, interactionLayer))
        {
            Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
            if (interactable.isInteractable)
            {
                interactable.DisplayPrompt();
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

        UIManager.instance.ClearText();
    }
}
