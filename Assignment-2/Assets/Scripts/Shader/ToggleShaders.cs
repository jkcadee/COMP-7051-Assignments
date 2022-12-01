using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShaders : MonoBehaviour
{
    public GameObject cam;
    bool fogOn = false;
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
