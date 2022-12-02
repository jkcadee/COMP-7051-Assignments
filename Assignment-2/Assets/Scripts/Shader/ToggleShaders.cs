using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShaders : MonoBehaviour
{
    public GameObject cam;
    public Light flashlight;
    bool fogOn = false;
    bool flashLightOn = false;
    EnableDepthMapAndBlit fogScript;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        fogScript = cam.GetComponent<EnableDepthMapAndBlit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
       {
            fogOn = !fogOn;
       }

       if (Input.GetKeyDown(KeyCode.E))
       {
            flashLightOn = !flashLightOn;
       }

       if(flashLightOn){
            flashlight.enabled = true;
       }

       if(!flashLightOn){
          flashlight.enabled = false;  
       }


       if(fogOn){
            fogScript.enabled = true;
            MusicController.Instance.ActivateFogMode();
       }
       if(!fogOn){
            fogScript.enabled = false;
            MusicController.Instance.DeactivateFogMode();
       }
    }
}
