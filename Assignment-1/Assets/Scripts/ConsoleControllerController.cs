using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


// This video was used predominantly to implement the console for this assignment:
// https://youtu.be/VzOEM-4A2OM

// CONSOLE COMMANDS:
// change_bg_green
// change_paddle_orange

public class ConsoleControllerController : MonoBehaviour
{
    bool showConsole;
    string input;
    private InputAction consoleToggleInput;
    private InputAction runCommandInput;
    private Actions inputActions;

    public static ConsoleCommand CHANGE_BG_COLOUR_GREEN;
    public static ConsoleCommand CHANGE_PADDLE_COLOUR_ORANGE;
    public List<object> commandList;

    public GameObject ground;
    public GameObject paddle1;
    public GameObject paddle2;
    
    public void OnToggleConsole(InputAction.CallbackContext obj)
    {
        showConsole = !showConsole;
    }

    public void OnReturnCommand(InputAction.CallbackContext obj)
    {
        if (showConsole)
        {
            RunCommand();
            input = "";
        }

    }

    private void Awake()
    {
        inputActions = new Actions();
        consoleToggleInput = inputActions.Player.ToggleConsole;
        runCommandInput = inputActions.Player.RunCommand;

        CHANGE_BG_COLOUR_GREEN = new ConsoleCommand("change_bg_green", "Changes the background panel to be green", "change_bg_green", () =>
        {
            ground.GetComponent<Renderer>().material.color = Color.green;
        });

        CHANGE_PADDLE_COLOUR_ORANGE = new ConsoleCommand("change_paddle_orange", "Changes the paddle colours to orange", "change_paddle_orange", () =>
        {
            GameObject[] paddles = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject paddle in paddles)
            {
                paddle.GetComponent<Renderer>().material.color = new Color(0.878f, 0.427f, 0.129f, 1f);
            }
        });


        commandList = new List<object>
        {
            CHANGE_BG_COLOUR_GREEN,
            CHANGE_PADDLE_COLOUR_ORANGE
        };
    }

    private void OnEnable()
    {
        consoleToggleInput.performed += OnToggleConsole;
        consoleToggleInput.Enable();

        runCommandInput.performed += OnReturnCommand;
        runCommandInput.Enable();

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

    private void RunCommand()
    {
        for(int i=0; i < commandList.Count; i++)
        {
            ConsoleCommandBase commandBase = commandList[i] as ConsoleCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i] as ConsoleCommand != null) // we are checking if the object type fits the cast here (ConsoleCommandBase)
                {
                    (commandList[i] as ConsoleCommand).Invoke(); // if it does, we cast back to ConsoleCommand and invoke it
                }
            }
        }
    }
}
