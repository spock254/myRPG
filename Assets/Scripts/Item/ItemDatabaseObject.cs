using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object",
                 menuName = "InventorySystem/Item/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ObjectItem[] items;

    // make ass func in future
    public Dictionary<int, ObjectItem> GetItem = new Dictionary<int, ObjectItem>(); /* get item by id */
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < items.Length; i++)
        {
            items[i].id = i;
            GetItem.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ObjectItem>();
    }


}
