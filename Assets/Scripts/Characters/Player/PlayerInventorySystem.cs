using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventorySystem : MonoBehaviour
{
    // move it TODO
    public MouseItem mouseItem = new MouseItem();

    public InventoryObject inventory;
    public InventoryObject equipmentInventory;

    bool isPickable = false;
    GroundItem itemToPick = null;
    GameObject goToDestroy = null;
    private void Start()
    {
        /* inventory assert */
        if (inventory == null) 
        {
            Debug.LogError("inventory is null");
        }
    }

    void Update()
    {
        //for test!!! save player inv
        if (Input.GetKeyDown(KeyCode.F5))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            inventory.Load();
        }

        /* pick item */
        if (isPickable == true) 
        {
            if (itemToPick != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //check for closet item

                    inventory.AddItem(new Item(itemToPick.item), itemToPick.amount);
                    Destroy(goToDestroy);
                }
            }
            else
            {
                /* item assert*/
                Debug.LogError("item is null");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Item")) //make Item name global readonly
        {
            Debug.Log("Item detected");

            itemToPick = collision.GetComponent<GroundItem>();
            goToDestroy = collision.gameObject;
            isPickable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Item")) //make Item name global readonly
        {
            isPickable = false;
        }
    }

    /* clear all item from player inventory when quit the game */
    void OnApplicationQuit()
    {
        /* removes also all allowed items */
        inventory.container.items = new SlotObject[Global.SLOTS_COUNT];
        
        /* allowed items not removes */
        equipmentInventory.container.Clear();
    }
}
