using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {

    public GameObject hammer;
    public GameObject screwers;
    public ItemSlot slot0;
    public ItemSlot slot1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("m"))
        {
            print("preso il martello");
            AddToInventory(hammer);
   
        }

        if (Input.GetKeyDown("c"))
        {
            print("presi i chiodi");
            AddToInventory(screwers);
        }

    }

    public void AddToInventory(GameObject obj)
    {
        if (slot0.GetComponent<Image>().enabled == false)
            slot0.UpdateSprite(obj);
        else slot1.UpdateSprite(obj);
    }

  
}
