using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Food")]
public class FoodItem : ObjectItem
{
    //some stats like recoveryItem
    private void Awake()
    {
        typeOfItem = ItemType.Food;
    }
}
