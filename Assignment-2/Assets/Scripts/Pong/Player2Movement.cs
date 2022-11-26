using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Movement : MonoBehaviour
{
    //Represents the input actions object.
    private PongActions inputActions;

    //Represents the input action object.
    private InputAction movement;

    //Represents the maximum z value of the player object.
    private float upperBound = 22.0f;

    //Represents the minimum z value of the player object
    private float lowerBound = -22.0f;

    //Represents the rigidbody component of the player.
    Rigidbody rb;

    /** 
        Represents the player when the awake state is called.
     */

    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); //get rigidbody, responsible for enabling collision with other colliders

        inputActions = new PongActions(); //create new InputActions
    }

    /** 
        Represents when the player's movement is enabled.
     */

    private void OnEnable()
    {
        movement = inputActions.Player2.Movement; //get reference to movement action
        movement.Enable();
    }

    /** 
        Represents when the player's movement is disabled.
     */

    private void OnDisable()
    {
        movement.Disable();
    }
    //called every physics update
    private void FixedUpdate()
    {
        //If the player's z value is greater than the upperbound and less than the lower bound,
        //the player's z value will be readjusted accordingly so that they are still within the
        // z value range.
        // if (transform.position.z >= upperBound || transform.position.z <= lowerBound)
        // {
        //     //If the z value is higher, lessen the z value by 1.
        //     if (transform.position.z >= upperBound)
        //     {
        //         Vector3 v3 = new Vector3(1, 0, 0);
        //         transform.Translate(v3);

        //     }
        //     //If the z value is lower, increase the z value by 1.
        //     else
        //     {

        //         Vector3 v3 = new Vector3(-1, 0, 0);
        //         transform.Translate(v3);

        //     }

        // }
        // //If the player's z value is within the range, the player can move within it.
        // else
        // {

        Vector2 v2 = movement.ReadValue<Vector2>();
        Vector3 v3 = new Vector3(-v2.y, 0, 0); //convert to 3d space
        transform.Translate(v3);
        Vector3 pos = transform.position;
        pos.z = Mathf.Clamp(pos.z, lowerBound, upperBound);
        transform.position = pos;

    }
}
// }
