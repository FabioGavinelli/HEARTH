using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithRobot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Robot")
        {
            Debug.Log("COLLISION");
            this.GetComponent<Rigidbody>().AddForce(new Vector3(100f,100f,100f), ForceMode.Impulse);
            Destroy(this.transform.gameObject, 2f);
        }
    }

}
