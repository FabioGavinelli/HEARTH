using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FinishRobotTrigger : MonoBehaviour
{
    [SerializeField] private GameObject robot;
    private NavMeshAgent navMeshAgent;
    private bool exit = true;

    private void Start()
    {
        navMeshAgent = robot.GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            navMeshAgent.isStopped = exit;
            robot.GetComponent<RobotAnimationController>().TriggerAnimation((int)RobotAnimationController.robotAnimations.walk);
            exit = !exit;
        }
    }
}
