using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Helment,
    Weapon,
    Shild,
    Chest,
    Boots,
    Default,
    Craft
}
public enum Attrebutes
{
    Agility,
    Intelect,
    Stamina,
    Strength
}

public abstract class ObjectItem : ScriptableObject
{
    public int id;
    public Sprite uiDisplay;
    public ItemType typeOfItem;
    [TextArea(15,20)]
    public string Description;

    public ItemBuff[] buffs;
    //add stacable bool value

    public Item CreateItem() 
    {
        return new Item(this);
    }
}
[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public ItemBuff[] buffs;

    public Item() 
    {
        this.name = "";
        this.id = Global.DEFAULT_ID;
    }

    public Item(ObjectItem objectItem) 
    {
        name = objectItem.name;
        id = objectItem.id;

        buffs = new ItemBuff[objectItem.buffs.Length];

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(objectItem.buffs[i].min, objectItem.buffs[i].max);
            buffs[i].attrebute = objectItem.buffs[i].attrebute;
        }
    }
}
[System.Serializable]
public class ItemBuff 
{
    public Attrebutes attrebute;
    public int value;
    public int min;
    public int max;
    public ItemBuff(int min, int max) 
    {
        this.min = min;
        this.max = max;
        GenerateValue();
    }

    public void GenerateValue() 
    {
        value = UnityEngine.Random.Range(min, max);
    }
}