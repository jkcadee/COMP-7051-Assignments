using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleShaders : MonoBehaviour
{
    public GameObject cam;
    bool fogOn = false;
    EnableDepthMapAndBlit script;

    


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        script = cam.GetComponent<EnableDepthMapAndBlit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
       {
            fogOn = !fogOn;
       }

       if(fogOn){
            script.enabled = true;
       }
       if(!fogOn){
            script.enabled = false;
       }
    }
}
