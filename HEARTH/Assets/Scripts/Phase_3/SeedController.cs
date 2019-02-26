using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    [SerializeField] private int numberOfSeeds;

    private void Update()
    {
        this.transform.Rotate(new Vector3(0,0,1), 1);
    }

    public int GetNumeberOfSeeds()
    {
        return numberOfSeeds;
    }
}
