using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Chest")]
public class ChestItem : ObjectItem
{
    private void Awake()
    {
        //fix later TODO
        typeOfItem = ItemType.Chest;
    }
}
