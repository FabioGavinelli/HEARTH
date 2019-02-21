using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedController : MonoBehaviour
{
    [SerializeField] private int numberOfSeeds;

    public int GetNumeberOfSeeds()
    {
        return numberOfSeeds;
    }
}
