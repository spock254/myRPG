using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();

    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    [SerializeField]
    Vector2 slotOfsets = new Vector2(0, 0);
    [SerializeField]
    Vector2 slotStart = new Vector2(0, 0);

    public int columNumbers = 5;
    Dictionary<GameObject, SlotObject> itemsDisplay = new Dictionary<GameObject, SlotObject>();
    // Start is called before the first frame update
    void Start()
    {
        //check if inv not null

        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    public void CreateSlots()
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

    public void UpdateSlots()
    {
        foreach (var slot in itemsDisplay)
        {
            if (slot.Value.id > Global.DEFAULT_ID)
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite =
                    inventory.itemDatabase.GetItem[slot.Value.item.id].uiDisplay;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text =
                    slot.Value.amount == 0 ? "" : slot.Value.amount.ToString("n0");
            }
            else 
            {
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }

    void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action) 
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj) 
    {
        mouseItem.hoverObj = obj;

        if (itemsDisplay.ContainsKey(obj)) 
        { 
            mouseItem.hoverItem = itemsDisplay[obj];
        }
    }

    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    }

    public void OnDragStart(GameObject obj)
    {
        //TODO every entry creates new go 
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(2.15f, 2.15f); //check if correct size
        mouseObject.transform.SetParent(transform.parent);

        /* if slot not empty */
        if (itemsDisplay[obj].id > Global.DEFAULT_ID) 
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.itemDatabase.GetItem[itemsDisplay[obj].id].uiDisplay;
            img.raycastTarget = false;

            Vector3 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition2D.z = 0;
            mouseObject.GetComponent<RectTransform>().position = mousePosition2D;
        }

        mouseItem.obj = mouseObject;
        mouseItem.item = itemsDisplay[obj];
    }

    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverObj != null)
        {
            /* swap items */
            inventory.MoveItem(itemsDisplay[obj], itemsDisplay[mouseItem.hoverObj]);
        }
        else 
        {
            inventory.RemoveItem(itemsDisplay[obj].item);
        }

        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }

    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
        {
            Debug.Log("not null");
            //mouseItem.obj.GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition2D.z = 0;
            mouseItem.obj.GetComponent<RectTransform>().position = mousePosition2D;
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(slotStart.x + (slotOfsets.x * (i % columNumbers)), slotStart.y + (-slotOfsets.y * (i / columNumbers)), 0f);
    }
}
