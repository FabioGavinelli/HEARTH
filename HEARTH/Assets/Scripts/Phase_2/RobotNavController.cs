using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class RobotNavController : MonoBehaviour {

    
    [SerializeField] public GameObject target;

    private NavMeshAgent navMeshAgent;
    private bool following = true;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        //se il robot sta già inseguendo il player
        if (following)
        {
            navMeshAgent.SetDestination(target.transform.position);
            
            //se ha raggiunto il target lo fa scomparire TOFIX
            if (TargetReached())
            {
                //GAMEOVER TODO
                Debug.Log("GAMEOVER");
            }
        }

    }

    //restituisce true se il target è raggiunto 
    private bool TargetReached()
    {
        if (!navMeshAgent.pathPending)
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    return true;

        return false;
    }

}
