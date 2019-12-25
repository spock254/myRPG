using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Craft")]
public class CraftItem : ObjectItem
{
    private void Awake()
    {
        typeOfItem = ItemType.Craft;
    }
}
