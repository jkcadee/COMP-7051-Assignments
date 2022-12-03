using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShaders : MonoBehaviour
{
    public GameObject fogCube;
    public Light flashlight;
    bool fogOn = false;
    bool flashLightOn = false;

    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
       {
            Debug.Log("FOG");
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
            fogCube.SetActive(true);
            MusicController.Instance.ActivateFogMode();
       }
       if(!fogOn){
            fogCube.SetActive(false);
            MusicController.Instance.DeactivateFogMode();
       }
    }
}
