using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{

    private NavMeshSurface navSurface;
    private GameObject maze;


    [SerializeField]
    NavMeshSurface[] navMeshSurfaces;


    // Start is called before the first frame update
    void Awake()
    {
        maze = GameObject.Find("Maze");

        foreach (Transform wall in maze.transform) {
            foreach (Transform quad in wall) {
            navSurface = quad.gameObject.AddComponent<NavMeshSurface>() as NavMeshSurface;
            }
        }

        navMeshSurfaces = FindObjectsOfType<NavMeshSurface>();

        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }


}
