using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    private float angle = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time, 3),  3.70f + Mathf.PingPong(Time.time, 1), 204f + Mathf.PingPong(Time.time, 0.5f));
        //transform.rotation = new Quaternion(Mathf.PingPong(transform.eulerAngles.x, 15), transform.eulerAngles.y, transform.eulerAngles.z, 0);


    }
}
