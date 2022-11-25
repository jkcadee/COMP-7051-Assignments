using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AT_CharMovement : MonoBehaviour
{
    InputActions inputActions;
    InputAction movement;
    Rigidbody rb;
    public float acceleration = 3f;
    public float maxSpeed = 15f;
    public bool isSoundPlaying;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputActions();
        movement = inputActions.Player.Movement;
        isSoundPlaying = false;
    }

    private void OnEnable()
    {
        movement.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
    }

    private void Update()
    {
        Vector2 v2 = movement.ReadValue<Vector2>();
        Vector3 v3 = new Vector3(v2.x * acceleration, 0, v2.y * acceleration);
        v3 *= Time.deltaTime * 20;
        if (v2.x == 0.0f && v2.y == 0.0f)
        {
            SFXController.Instance.StopSound(0);
            isSoundPlaying = false;
        }
        else {
            if (!isSoundPlaying) {
                SFXController.Instance.PlaySound(0);
                isSoundPlaying = true;
            }
        }
        rb.AddForce(transform.rotation * v3, ForceMode.VelocityChange);
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall") {
            SFXController.Instance.PlaySound(1);
        }
    }

}
