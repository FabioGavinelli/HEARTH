using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    private float angle = 1f;

    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time, 3),  3f + Mathf.PingPong(Time.time, 1), 204f + Mathf.PingPong(Time.time, 0.5f));     
    }
}
