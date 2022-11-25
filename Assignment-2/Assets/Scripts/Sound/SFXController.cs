using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    /** 
         * Sources for code.
         * 
         * Maintaining data persistance between scenes was done using this resource:
            https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#

            The use of multiple audio sources in code was done using this resource:
            https://answers.unity.com/questions/1320031/having-multiple-audio-sources-in-a-single-object.html
    */

    public static SFXController Instance;

    /** 
     Upon awaking, the class will instantiate once.
     */
    private void Awake() {

        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (gameObject.GetComponents<AudioSource>().Length > 0)
        {
            Debug.Log("There is some audio");
        }
        else {
            Debug.Log("There is no audio");
        }

    }

    /** Takes in an integer, and plays the sound that matches the index.
     @param i
    */

    public void PlaySound(int i) {

        if (i >= 0)
        {
            if (i < gameObject.GetComponents<AudioSource>().Length)
            {
                gameObject.GetComponents<AudioSource>()[i].Play();
                Debug.Log("Search is Valid");
            }
            else {
                Debug.Log("Search value is out of bounds");
            }
        }
        else {
            Debug.Log("Search value cannot be zero");
        }
    }
    
}
