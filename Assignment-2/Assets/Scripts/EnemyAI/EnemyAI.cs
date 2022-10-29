using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    public List<Transform> waypoints;
    public int waypointIndex;
    public Vector3 target;
    private int randomXCord;
    private int randomZCord;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        waypointIndex = 0;

        while(waypoints.Count < 2) {
            CretaeWaypoints();

            waypoints = waypoints.Distinct().ToList();
        }

        UpdateDestination();

    }



    // Update is called once per frame
    void Update()
    {


        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateWaypoint();
            UpdateDestination();
        }

    }

    void UpdateDestination()
    {
        target = waypoints[waypointIndex].position;
        _agent.SetDestination(target);
    }

    void IterateWaypoint()
    {
        waypointIndex++;
        if (waypointIndex == waypoints.Count)
        {
            waypointIndex = 0;
        }
    }

    bool CheckEmptySpace()
    {
        randomXCord = Random.Range(-2, 3);
        randomZCord = Random.Range(-2, 3);
        Debug.Log("X: " + randomXCord + "; " + "Z: " + randomZCord);
        Vector3 rayPosition = new Vector3(randomXCord, 20f, randomZCord);
        RaycastHit hit;
        if (Physics.Raycast(rayPosition, Vector3.down, out hit, 19.5f))
        {

            return false;
        }
        else
        {

            return true;
        }
    }

    void CretaeWaypoints()
    {
        if (CheckEmptySpace()) {
        GameObject waypoint = new GameObject("Waypoint");
        waypoint.transform.position = new Vector3(randomXCord, 0.5f, randomZCord);
        waypoints.Add(waypoint.transform);
        }

    }
}
