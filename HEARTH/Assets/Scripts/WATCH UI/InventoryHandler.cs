using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {

    public ItemSlot [] slots;
    public My_FPSInteractionManager my_fpsInter;

    public int num_objects;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {


    }

    public void AddToInventory(GameObject obj)
    {
        if (num_objects < slots.Length)
        {
            slots[num_objects].UpdateSprite(obj);
            num_objects++;
        }
        return;
    }

  
}
