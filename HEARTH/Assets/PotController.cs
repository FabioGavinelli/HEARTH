using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    private bool plant = false;

    public void SetPlanted()
    {
        plant = true;
    }

    public bool GetPlanted()
    {
        return plant;
    }
}
