
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Helmet")]
public class HelmeItem : ObjectItem
{
    private void Awake()
    {
        typeOfItem = ItemType.Helment;
    }
}
