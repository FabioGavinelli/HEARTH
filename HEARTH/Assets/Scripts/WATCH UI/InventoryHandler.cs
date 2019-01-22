using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {

    public GameObject hammer;
    public GameObject screwers;
    public ItemSlot slot0;
    public ItemSlot slot1;
    public My_FPSInteractionManager my_fpsInter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (my_fpsInter.GetOwningHammer() == true)
        { 
            AddToInventory(hammer);  
        }

        if (my_fpsInter.GetOwningNails() == true)
        {
            AddToInventory(screwers);
        }

    }

    public void AddToInventory(GameObject obj)
    {
        if (slot0.GetComponent<Image>().enabled == false)
            slot0.UpdateSprite(obj);
        else
            slot1.UpdateSprite(obj);
    }

  
}
