using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithRobot : MonoBehaviour
{
    private Transform startLocation;

    private void Start()
    {
        startLocation = this.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Robot")
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(100f,100f,100f), ForceMode.Impulse);
            Destroy(this.transform.gameObject, 2f);
        }
    }

    public void ResetAfterGameover()
    {
        Instantiate(this.transform.gameObject, startLocation.position, startLocation.rotation);
    }


}
