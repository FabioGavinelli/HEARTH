using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnRobot : MonoBehaviour
{
    [SerializeField] private GameObject robot; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            robot.SetActive(true);
            Rigidbody r_rigid = robot.GetComponent<Rigidbody>();
            r_rigid.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX |
                                  RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY |
                                  RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
