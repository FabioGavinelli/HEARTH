using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2Conroller : MonoBehaviour
{
    [SerializeField] private GameObject robot;

    // Start is called before the first frame update
    void Start()
    {
        robot.GetComponent<RobotNavController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
