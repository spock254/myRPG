using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Shild")]
public class ShildItem : ObjectItem
{
    private void Awake()
    {
        typeOfItem = ItemType.Shild;
    }
}
