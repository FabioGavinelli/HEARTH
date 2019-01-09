using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineObject : MonoBehaviour {

    public Material[] materials;
    MeshRenderer rend;

    // Use this for initialization
    void Start () {
        //standard = Shader.Find("Standard");
        //outlined = Shader.Find("Outlined/Silhouette Diffuse");
        rend = this.gameObject.GetComponent<MeshRenderer>();
        rend.sharedMaterial = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
   /* private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            rend.sharedMaterial = materials[1];
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            rend.sharedMaterial = materials[0];
        }
    }*/

    public void OutlineObj(bool outlined)
    {
        if (outlined == true)
        {
            rend.sharedMaterial = materials[1];
        }
        else
        {
            rend.sharedMaterial = materials[0];
        }
    }
}
