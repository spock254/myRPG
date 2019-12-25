using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;

    [SerializeField]
    Vector2 slotOfsets = new Vector2(0, 0);
    [SerializeField]
    Vector2 slotStart = new Vector2(0, 0);
    [SerializeField]
    int columNumbers = 5;

    public override void CreateSlots()
    {
        itemsDisplay = new Dictionary<GameObject, SlotObject>();

        for (int i = 0; i < inventory.container.items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            /* event registration */
            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            itemsDisplay.Add(obj, inventory.container.items[i]);
        }
    }

    private Vector3 GetPosition(int i)
    {
        return new Vector3(slotStart.x + (slotOfsets.x * (i % columNumbers)), slotStart.y + (-slotOfsets.y * (i / columNumbers)), 0f);
    }
}
