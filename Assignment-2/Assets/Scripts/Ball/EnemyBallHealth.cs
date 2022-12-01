using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBallHealth : MonoBehaviour
{
    int health = 3;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ball")
        {
            health--;
            if(health < 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
