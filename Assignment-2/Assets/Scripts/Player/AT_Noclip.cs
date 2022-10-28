using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AT_Noclip : MonoBehaviour
{
    InputActions inputActions;
    InputAction noclip;
    int ncLayer;
    int dfLayer;

    private void Awake()
    {
        inputActions = new InputActions();
        noclip = inputActions.Player.Noclip;
        dfLayer = LayerMask.NameToLayer("Default");
        ncLayer = LayerMask.NameToLayer("Noclip");
    }

    private void OnEnable()
    {
        noclip.Enable();
        noclip.performed += ToggleNoclip;
    }

    private void OnDisable()
    {
        noclip.Disable();
        noclip.performed -= ToggleNoclip;
    }

    private void ToggleNoclip(InputAction.CallbackContext _)
    {
        gameObject.layer = gameObject.layer == ncLayer ? dfLayer : ncLayer;
    }
}
