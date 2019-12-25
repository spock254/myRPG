using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Weapon")]
public class WeaponItem : ObjectItem
{
    private void Awake()
    {
        typeOfItem = ItemType.Weapon;
    }
}
