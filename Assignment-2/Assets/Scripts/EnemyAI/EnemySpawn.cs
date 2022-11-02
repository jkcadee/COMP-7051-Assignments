using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy() {

        yield return new WaitForSeconds(1f);
        Instantiate(enemy, Vector3.zero, Quaternion.identity);
        
    }
}
