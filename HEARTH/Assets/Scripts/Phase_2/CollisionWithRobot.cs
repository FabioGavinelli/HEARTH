using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithRobot : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Robot")
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(100f,100f,100f), ForceMode.Impulse);
            Destroy(this.transform.gameObject, 2f);
        }
    }

}
