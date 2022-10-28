using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AT_BodyRotate : MonoBehaviour
{
    public float speed = 4f;
    InputActions inputActions;
    InputAction looking;

    private void Awake()
    {
        inputActions = new InputActions();
        looking = inputActions.Player.Looking;
    }

    private void OnEnable()
    {
        looking.Enable();
    }

    private void OnDisable()
    {
        looking.Disable();
    }


    void Update()
    {
        float lookRotation = Input.GetAxis("Mouse X") * speed;
        Vector2 v2 = looking.ReadValue<Vector2>();
        transform.Rotate(0, lookRotation + v2.x/2, 0);
    }
}
