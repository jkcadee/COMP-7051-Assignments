using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBallHealth : MonoBehaviour
{
    int health = 3;
    float purgatoryTimeout = 0;
    float y;
    public GameObject enemyPrefab;
    public AudioSource enemyDeath;
    public AudioSource enemySpawn;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ball")
        {
            health--;
            if(health < 1)
            {
                purgatoryTimeout = 5;
                y = transform.position.y;
                GetComponent<NavMeshAgent>().enabled = false;
                transform.position = new Vector3(1000, -100, 1000);
                enemyDeath.Play();
                Debug.Log("ENEMY DYING!!!");
            }
        }
    }

    private void Update()
    {
        if(purgatoryTimeout > 0)
        {
            purgatoryTimeout -= Time.deltaTime;
        }
        else if (purgatoryTimeout < 0)
        {

            purgatoryTimeout = 0;
            int x = Random.Range(-2, 2);
            int z = Random.Range(-2, 2);
            enemySpawn.Play();
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, y, z), Quaternion.identity);

            enemy.GetComponent<NavMeshAgent>().enabled = true;
            Destroy(gameObject);
            
        }
    }
}
