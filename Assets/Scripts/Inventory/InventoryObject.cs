using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Inventory",
                 menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject itemDatabase;
    public Inventory container;

    public void AddItem(Item item, int amount)
    {
        /* use dictionary in future */

        // if item has buffs
        if (item.buffs.Length > 0)
        {
            SetEmptySlot(item, amount);
            return;
        }

        for (int i = 0; i < container.items.Length; i++)
        {
            if (container.items[i].id == item.id)
            {
                //check if amount is sute 
                container.items[i].AddAmount(amount);
                return;
            }
        }

        SetEmptySlot(item, amount);
    }

    public SlotObject SetEmptySlot(Item item, int amount)
    {
        for (int i = 0; i < container.items.Length; i++)
        {
            if (container.items[i].id <= Global.DEFAULT_ID)
            {
                container.items[i].UpdateSlot(item.id, item, amount);
                return container.items[i];
            }
        }

        // when inventory is full 
        return null;
    }

    public void RemoveItem(Item item) 
    {
        for (int i = 0; i < container.items.Length; i++)
        {
            if (container.items[i].item == item)
            {
                container.items[i].UpdateSlot(Global.DEFAULT_ID, null, 0);
            }
        }
    }

    public void MoveItem(SlotObject item1, SlotObject item2) 
    {
        SlotObject temp = new SlotObject(item2.id, item2.item, item2.amount);
        item2.UpdateSlot(item1.id, item1.item, item1.amount);
        item1.UpdateSlot(temp.id, temp.item, temp.amount);
    }

    [ContextMenu("Save")]
    public void Save() 
    {
        IFormatter formatter = new BinaryFormatter();
        //check if file created success
        Stream stream = new FileStream(Global.PLAYER_ITEM_TO_SAVE_PATH, FileMode.Open);
        formatter.Serialize(stream, container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load() 
    {
        if (File.Exists(Global.PLAYER_ITEM_TO_SAVE_PATH) == true)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stram = new FileStream(Global.PLAYER_ITEM_TO_SAVE_PATH, FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stram);

            for (int i = 0; i < container.items.Length; i++)
            {
                container.items[i].UpdateSlot(newContainer.items[i].id, newContainer.items[i].item, newContainer.items[i].amount);
            }

            stram.Close();
        }
        else 
        { 
            // tell user file not exist
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        container.Clear();
    }
}


[System.Serializable]
public class Inventory
{
    public SlotObject[] items = new SlotObject[Global.SLOTS_COUNT];
    public void Clear() 
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].UpdateSlot(Global.DEFAULT_ID, new Item(), 0);
        }
    }
}

[System.Serializable]
public class SlotObject
{
    public ItemType[] AllowedItems = new ItemType[0];
    public UserInterface parent;
    public Item item;
    public int amount;
    public int id = Global.DEFAULT_ID; // -1 change in future part of inventory scope of view

    public SlotObject()
    {
        this.id = Global.DEFAULT_ID;
        this.item = null;
        this.amount = 0;
    }

    public SlotObject(int id, Item item, int amount)
    {
        this.id = id;
        this.item = item;
        this.amount = amount;
    }
    public void UpdateSlot(int id, Item item, int amount)
    {
        this.id = id;
        this.item = item;
        this.amount = amount;
    }

    public bool AddAmount(int amont)
    {
        //check if less or eq to max amount (64)
        this.amount += amont;
        return true;
        //if more retuen false
    }

    public bool CanPlaceInSlot(ObjectItem item) 
    {
        if (AllowedItems.Length <= 0)
        {
            return true;
        }

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (item.typeOfItem == AllowedItems[i])
            {
                return true;
            }
        }

        return false;
    }
}