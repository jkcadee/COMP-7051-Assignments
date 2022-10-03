using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed = 3f;

    private float upperBound = 20.0f;
    private float lowerBound = -20.0f;

    private CharacterController controller;

    private Vector3 moveDirection = Vector3.zero;
    private Vector2 inputVector = Vector2.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void SetInputVector(Vector2 direction)
    {
        inputVector = direction;
    }

    void FixedUpdate()
    {
        moveDirection = new Vector3(-inputVector.y, 0, 0);
        // Vector3 v3 = new Vector3(-1, 0, 0);
        // transform.Translate(moveDirection);
        // moveDirection = transform.TransformDirection(moveDirection);
        // moveDirection *= MoveSpeed;

        // controller.Move(moveDirection * Time.deltaTime);
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

            // Vector2 v2 = movement.ReadValue<Vector2>();
            // Vector3 v3 = new Vector3(-v2.y, 0, 0); //convert to 3d space
            transform.Translate(moveDirection);

        }
    }
}
