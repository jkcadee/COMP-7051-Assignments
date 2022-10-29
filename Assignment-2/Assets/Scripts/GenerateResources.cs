using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateResources : MonoBehaviour
{

    public GameObject gamePrefab;
    public Vector3 placementVector;

    void Start()
    {
        Instantiate(gamePrefab, placementVector, Quaternion.identity);
    }

}
