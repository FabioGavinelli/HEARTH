using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToInventory : MonoBehaviour
{

    [SerializeField] private InventoryHandler IH;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory()
    {
        IH.AddToInventory(this.gameObject);
    }
}
