using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object", 
                 menuName = "InventorySystem/Item/Default")]
public class DefaultItem : ObjectItem
{
    private void Awake()
    {
        typeOfItem = ItemType.Default;
    }
}
