using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    /** 
         * Sources for code.
         * 
         * Maintaining data persistance between scenes was done using this resource:
            https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#

            The use of multiple audio sources in code was done using this resource:
            https://answers.unity.com/questions/1320031/having-multiple-audio-sources-in-a-single-object.html
    */

    public static MusicController Instance;
    public static AudioSource levelMusic;
    public static float volume;

    /** 
     Upon awaking, the class will instantiate once.
     */
    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        volume = 1.0f;
    }

    private void Update() {

        if (!IsMusicNull()) {

            levelMusic.volume = volume;

        }

    }

    /** Takes in an integer, and plays the sound that matches the index.
     @param i
    */

    public void SelectMusic(int i)
    {
        if (i >= 0)
        {
            if (i < gameObject.GetComponents<AudioSource>().Length)
            {
                levelMusic = gameObject.GetComponents<AudioSource>()[i];
                Debug.Log("Search is Valid");
            }
            else
            {
                Debug.Log("Search value is out of bounds");
            }
        }
        else
        {
            Debug.Log("Search value cannot be zero");
        }
    }

    public void PlayMusic() {
        if (!IsMusicNull()) {
            levelMusic.Play();
        }
    }

    public void PauseMusic() {
        if (!IsMusicNull())
        {
            levelMusic.Pause();
        }
    }

    public void StopMusic() {
        if (!IsMusicNull())
        {
            levelMusic.Stop();
        }
    }

    public void IncreaseVolume() {
        if (volume < 1.0f)
        {
            volume += 0.1f;
        }
        else {
            Debug.Log("Already at max volume.");
        }
    }

    public void DecreaseVolume()
    {
        if (volume > 0.0f)
        {
            volume -= 0.1f;
        }
        else
        {
            Debug.Log("Already at min volume.");
        }
    }

    public void SetVolume(float f) {

        if (f >= 0.0f && f <= 1.0f) {
            volume = f;
        }
    
    }

    public bool IsMusicNull() {

        if (levelMusic == null)
        {
            return true;
        }
        else {
            return false;
        }
    
    }

}
