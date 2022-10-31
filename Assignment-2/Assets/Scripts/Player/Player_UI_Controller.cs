using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_UI_Controller : MonoBehaviour
{
    public GameObject popUp;
    Rigidbody rb;
    Vector2 spawnPoint;
    private InputActions inputActions;

    // Start is called before the first frame update
    void Start()
    {
        deActivatePopup();
        spawnPoint = GetComponent<Rigidbody>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        inputActions.Player.Reset.performed += DoReset;
        inputActions.Player.Reset.Enable();
    }

    private void activatePopup() {

        popUp.SetActive(true);
    
    }

    private void deActivatePopup()
    {

        popUp.SetActive(false);

    }

    public void ResetPosition() {

        Debug.Log("Position Reset");
        GetComponent<Rigidbody>().transform.position = spawnPoint;

    }

    private void DoReset(InputAction.CallbackContext obj)
    {
        Debug.Log("Reset"); //called when jump performed
        ResetPosition();
    }
}
