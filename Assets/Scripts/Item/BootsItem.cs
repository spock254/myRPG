
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Boots")]
public class BootsItem : ObjectItem
{
    private void Awake()
    {
        typeOfItem = ItemType.Boots;
    }
}
