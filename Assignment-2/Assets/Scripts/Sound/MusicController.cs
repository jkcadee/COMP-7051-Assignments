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

    //Represents the single instance of this static class.
    public static MusicController Instance;

    //Represents the current audiosource being used as the music.
    public AudioSource levelMusic;

    //Represents the current volume of the music.
    public float volume;

    //Represents whether or not the music is on.
    public bool isOn;

    //Represents the maximum volume that the music can be.
    //Note that this varies depending whether or not fog mode
    //is on. If fog mode is on, the maximum is 0.5f, while if it
    //is off, it is 1.0f.
    public float maxVolume;

    //Represents the minimum volume of the music.
    //This value would be about 0.1f.
    public float minVolume;

    //Represents whether or not fog mode had been activated.
    public bool fogModeActivated;

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
        //Minimum volume will always be 0.1f
        minVolume = 0.1f;
        //Volume is set to minimum at start
        volume = minVolume;
        //Assumes Fog was deactivated, may change over time
        DeactivateFogMode();
        //Assumes Fog was activated, may change over time
        //ActivateFogMode();
        //TEMPORARY CODE FOR TESTING //
        //By default, the daytime music is selected, but this may not always be the case
        SelectMusic(0);
        //By default, the music will be on.
        isOn = true;
        //By default, the music is played.
        PlayMusic();
        //TEMPORARY CODE FOR TESTING //
    }

    /** Checks for updates in the volume levels, and readjusts the volume of the
     * music to that level. Detects whether or not the player has pressed the space key,
     * and if they have, it will turn the music on or off.
     */

    private void Update() {

        if (!IsMusicNull()) {
            levelMusic.volume = volume;
        }

        if (Input.GetKeyDown("space"))
        {
            if (!IsMusicNull()) {
                if (isOn)
                {
                    StopMusic();
                    isOn = false;
                }
                else {
                    PlayMusic();
                    isOn = true;
                }
            }
        }

    }

    /** Takes in an integer, and plays the sound that matches the index.
     * If the index does not exist, the search will not be done, and no new song
     * will be selected.
     @param i
    */

    public void SelectMusic(int i)
    {
        if (i >= 0)
        {
            if (i < gameObject.GetComponents<AudioSource>().Length)
            {
                levelMusic = gameObject.GetComponents<AudioSource>()[i];
                //Debug.Log("Search is Valid");
            }
            else
            {
                //Debug.Log("Search value is out of bounds");
            }
        }
        else
        {
            //Debug.Log("Search value cannot be less than zero");
        }
    }

    /** If there is currently music selected, this function will play it.*/

    public void PlayMusic() {
        if (!IsMusicNull()) {
            levelMusic.Play();
        }
    }

    /** If there is currently music selected, this function will pause it.*/

    public void PauseMusic() {
        if (!IsMusicNull())
        {
            levelMusic.Pause();
        }
    }

    /** If there is currently music selected, this function will stop it.*/

    public void StopMusic() {
        if (!IsMusicNull())
        {
            levelMusic.Stop();
        }
    }

    /** Sets the volume according to what was written in the parameter.
     * If the parameter is higher than the max volume, it will just be set to the max volume.
     * If the parameter is lower than the minimum volume, it will just be set to the min volume.
     * If the parameter is between the max volume and the min volume, it will be set to that number exactly.
     @param f
    */

    public void SetVolume(float f) {

        float playerinpt = f;

        if (fogModeActivated) {
            playerinpt *= 0.5f;
        }

        if (playerinpt >= minVolume && playerinpt <= maxVolume)
        {
            volume = playerinpt;
        }
        else {

            if (playerinpt >= maxVolume) {
                volume = maxVolume;
            }

            if (playerinpt <= minVolume) {
                volume = minVolume;
            }

        }
    
    }

    /** 
     Returns whether or not the levelMusic attribute has been assigned a value.
     If it has, it will return false. If it has not, it will return true.
     @return true
     @return false
     */

    public bool IsMusicNull() {

        if (levelMusic == null)
        {
            return true;
        }
        else {
            return false;
        }
    
    }

    /** 
     Sets the music settings to fog mode. Essentially, it sets the max volume to 0.5f, which is half of the
     regular max volume amount. In this case, the volume would be half the volume it would be otherwise.
     */

    public void ActivateFogMode() {
        maxVolume = 0.5f;
        fogModeActivated = true;
    }

    /** 
     Deactivates fog mode. Essentially, it sets the max volume back to 1.0f, and the sound operates as it
     normally would.
     */

    public void DeactivateFogMode() {
        maxVolume = 1.0f;
        fogModeActivated = false;
    }

}
