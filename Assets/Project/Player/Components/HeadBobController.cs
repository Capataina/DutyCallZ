using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] float idleHeight;
    [SerializeField] float idleSpeed;

    [SerializeField] float movingHeight;
    [SerializeField] float movingSpeed;
    [SerializeField] LayerMask eyeHitMask;

    float time = 0;

    private void Update()
    {
        if (playerMovement.isMoving())
        {
            float y = Mathf.Sin(time * movingSpeed);

            transform.localPosition = new Vector3(0, y * movingHeight, 0);

            Debug.Log("moving");

        }
        else
        {
            float y = Mathf.Sin(time * idleSpeed);

            transform.localPosition = new Vector3(0, y * idleHeight, 0);
        }


        time += Time.deltaTime % (Mathf.PI) * 2;
    }
}
