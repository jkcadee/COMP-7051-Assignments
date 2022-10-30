using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_UI_Controller : MonoBehaviour
{
    public GameObject popUp;
    
    // Start is called before the first frame update
    void Start()
    {
        deActivatePopup();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    
    }
}
