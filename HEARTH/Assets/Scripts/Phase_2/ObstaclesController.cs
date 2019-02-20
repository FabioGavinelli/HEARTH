using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesController : MonoBehaviour
{
    [SerializeField] private GameObject ObstaclesContainer;

    public void ResetCubes()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            ObstaclesContainer.transform.GetChild(i).GetComponent<CollisionWithRobot>().ResetAfterGameover();
        }
    }


}
