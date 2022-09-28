using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputActions inputActions;
    private InputAction movement;
    private float upperBound = 20.0f;
    private float lowerBound = -20.0f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); //get rigidbody, responsible for enabling collision with other colliders

        inputActions = new InputActions(); //create new InputActions
    }
    private void OnEnable()
    {
        movement = inputActions.Player.Movement; //get reference to movement action
        movement.Enable();
    }
    private void OnDisable()
    {
        movement.Disable();
    }
    //called every physics update
    private void FixedUpdate()
    {
        if (transform.position.z >= upperBound || transform.position.z <= lowerBound)
        {
            if (transform.position.z >= upperBound)
            {
                Vector3 v3 = new Vector3(1, 0, 0);
                transform.Translate(v3);

            }
            else {

                Vector3 v3 = new Vector3(-1, 0, 0);
                transform.Translate(v3);

            }

        }
        else {

            Vector2 v2 = movement.ReadValue<Vector2>();
            Vector3 v3 = new Vector3(-v2.y, 0, 0); //convert to 3d space
            transform.Translate(v3);

        }
    }
}
