using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Sword")]
public class SwordItem : ObjectItem
{
    private void Awake()
    {
        typeOfItem = ItemType.Sword;
    }
}
