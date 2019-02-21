using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour {

    public ItemSlot [] slots;
    public My_FPSInteractionManager my_fpsInter;

    private int num_objects;

	// Use this for initialization
	void Start () {
        num_objects = 0;
	}
	
	// Update is called once per frame
	void Update () {


    }

    public void AddToInventory(GameObject obj)
    {
        slots[num_objects].UpdateSprite(obj);
        num_objects++;
    }

  
}
