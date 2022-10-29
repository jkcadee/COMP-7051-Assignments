using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AT_CameraRotate : MonoBehaviour
{
    public float speed = 4f;
    public float lookRange = 45f;
    Vector3 rotation;
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
        float lookMagnitude = Input.GetAxis("Mouse Y") * speed;
        Vector2 v2 = looking.ReadValue<Vector2>();
        rotation = new Vector3(Mathf.Clamp(rotation.x - lookMagnitude - v2.y/2, -lookRange, lookRange), 0, 0);
        transform.localEulerAngles = rotation;
    }
}
