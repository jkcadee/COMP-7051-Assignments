using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugController : MonoBehaviour
{
    bool showConsole;
    string input;
    private InputAction consoleToggleInput;
    private Actions inputActions;

    private void Awake()
    {
        inputActions = new Actions();
        consoleToggleInput = inputActions.Player.ToggleConsole;
    }

    private void OnEnable()
    {
        consoleToggleInput.performed += OnToggleConsole;
        consoleToggleInput.Enable();
    }

    public void OnToggleConsole(InputAction.CallbackContext obj)
    {
        showConsole = !showConsole;
    }

    private void OnGUI()
    {
        if (!showConsole)
        {
            return;
        }

        float y = 0f;

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }
}
