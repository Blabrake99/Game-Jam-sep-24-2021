using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float waitTime = 2f;
    int patrolIndex;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(waypoints != null &&  waypoints.Count >= 2)
        {
            patrolIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(agent.remainingDistance <= 1f)
        {
            waitTime -= Time.deltaTime;
            ChangeWaypoint();
            ChangeDestination();
        }
       // agent.destination = waypoints.Count.position;
    }

    private void ChangeWaypoint()
    {
        patrolIndex++;

        if(patrolIndex >= waypoints.Count)
        {
            patrolIndex = 0;
        }
    }

    private void ChangeDestination()
    {
        if(waypoints != null)
        {
            Vector3 target = waypoints[patrolIndex].transform.position;
            agent.SetDestination(target);
            waitTime = 2f;
        }
    }


}
