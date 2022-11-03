using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerStayEvent : MonoBehaviour
{
    /** CODE WAS SOURCED FROM THE FOLLOWING:
    https://answers.unity.com/questions/867859/how-to-detect-ontriggerenter-on-another-gameobject.html
    */
    private string colTag;

    void Start()
    {
        colTag = "";
    }

    void Update() {

    }

    public void OnTriggerEnter(Collider col)
    {
        setColTag(col.gameObject.tag);
    }

    public void setColTag(string col) {
        colTag = col;
    }

    public string getColTag() {
        return colTag;
    }
}
