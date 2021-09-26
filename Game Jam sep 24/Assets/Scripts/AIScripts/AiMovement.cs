using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float waitTime = 5f;
    int waypointIndex;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();

        if (waypoints != null && waypoints.Count >= 2)
        {
            waypointIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        waitTime -= Time.deltaTime;

        if (waitTime < 0)
        {
            waitTime = 0;
        }

        if (agent.remainingDistance <= 1f && waitTime <= 0 && !gameObject.GetComponent<TalkibleNPC>().talking)
        {
            waitTime = 5f;
            ChangeWaypoint();
            ChangeDestination();
        }

        if (gameObject.GetComponent<TalkibleNPC>().talking || agent.remainingDistance <= .2f)
            anim.SetBool("IsWalking", false);
        if(agent.remainingDistance > .2f)
            anim.SetBool("IsWalking", true);

    }

    private void ChangeWaypoint()
    {
        waypointIndex++;

        if (waypointIndex >= waypoints.Count)
        {
            waypointIndex = 0;
        }
    }

    private void ChangeDestination()
    {
        if (waypoints != null)
        {
            Vector3 target = waypoints[waypointIndex].transform.position;
            agent.SetDestination(target);
        }
    }
}
