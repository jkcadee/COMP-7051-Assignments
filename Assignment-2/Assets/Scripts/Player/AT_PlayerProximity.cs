using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AT_PlayerProximity : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentMaxDistance = MaxDistance();
        if (currentMaxDistance != -1.0f)
        {
            MusicController.Instance.SetVolume(MusicController.Instance.maxVolume - currentMaxDistance);
        }
        else {
            MusicController.Instance.SetVolume(MusicController.Instance.minVolume);
        }
    }

    /** In cases where there is more than one enemy, the volume size will be
     determined by the distance to the closest enemy. If there are no enemies on the
    maze, the function will reutrn -1.0f as an error code.
    @return maxDistance
    @return -1.0f
    */

    private float MaxDistance() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            float maxDistance = Distance(gameObject.transform.position, enemies[0].transform.position);
            for (int i = 0; i < enemies.Length; i++) {
                float distance = Distance(gameObject.transform.position, enemies[i].transform.position);
                if (distance > maxDistance) {
                    maxDistance = distance;
                }
            }
            return maxDistance;
        }
        else {
            return -1.0f;
            //Debug.Log("No Enemies Have Been Found on the Playing field");
        }

    }

    /** 
     Calculates the distance between two positions, then returns it back.
    @param startPos
    @param endPos
    @return total
     */

    private float Distance(Vector3 startPos, Vector3 endPos) {
        float xVal = endPos.x - startPos.x;
        float yVal = endPos.y - startPos.y;
        float zVal = endPos.z - startPos.z;
        if (xVal < 0) {
            xVal *= -1.0f;
        }        
        
        if (yVal < 0) {
            yVal *= -1.0f;
        }        
        
        if (zVal < 0) {
            zVal *= -1.0f;
        }
        float total = (xVal + yVal + zVal) / 3.0f;
        return total;
    }
}
