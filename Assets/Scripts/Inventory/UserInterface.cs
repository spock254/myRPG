using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{
    // move it TODO
    public PlayerInventorySystem playerInventorySystem;

    public InventoryObject inventory;

    // better ake protected
    public Dictionary<GameObject, SlotObject> itemsDisplay = new Dictionary<GameObject, SlotObject>();
    // Start is called before the first frame update
    void Start()
    {
        //check if inv not null

        for (int i = 0; i < inventory.container.items.Length; i++)
        {
            inventory.container.items[i].parent = this;
        }

        CreateSlots();

        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    public abstract void CreateSlots();


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

    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void OnEnter(GameObject obj)
    {
        playerInventorySystem.mouseItem.hoverObj = obj;

        if (itemsDisplay.ContainsKey(obj))
        {
            playerInventorySystem.mouseItem.hoverItem = itemsDisplay[obj];
        }
    }

    public void OnExit(GameObject obj)
    {
        playerInventorySystem.mouseItem.hoverObj = null;
        playerInventorySystem.mouseItem.hoverItem = null;
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

        playerInventorySystem.mouseItem.obj = mouseObject;
        playerInventorySystem.mouseItem.item = itemsDisplay[obj];
    }

    public void OnDragEnd(GameObject obj)
    {
        var itemOnMouse = playerInventorySystem.mouseItem;
        var mouseHoverItem = itemOnMouse.hoverItem;
        var mouseHoverObject = itemOnMouse.hoverObj;
        var GetItemObject = inventory.itemDatabase.GetItem;

        if (itemOnMouse.ui != null)
        {
            if (mouseHoverItem != null)
            {
                /* swap items */
                //inventory.MoveItem(itemsDisplay[obj], itemsDisplay[playerInventorySystem.mouseItem.hoverObj]);

                /* to avoid bugs in future */
                if (mouseHoverItem.CanPlaceInSlot(GetItemObject[itemsDisplay[obj].id])
                    && (mouseHoverItem.item.id <= Global.DEFAULT_ID
                    || (mouseHoverItem.item.id >= 0
                    && itemsDisplay[obj].CanPlaceInSlot(GetItemObject[mouseHoverItem.item.id]))))
                {
                    inventory.MoveItem(itemsDisplay[obj], mouseHoverItem.parent.itemsDisplay[itemOnMouse.hoverObj]);
                }
            }
        }
        else
        {
            /* remove the item */
            //inventory.RemoveItem(itemsDisplay[obj].item);
        }

        Destroy(itemOnMouse.obj);
        playerInventorySystem.mouseItem.item = null;
    }

    public void OnDrag(GameObject obj)
    {
        if (playerInventorySystem.mouseItem.obj != null)
        {
            //mouseItem.obj.GetComponent<RectTransform>().position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition2D.z = 0;
            playerInventorySystem.mouseItem.obj.GetComponent<RectTransform>().position = mousePosition2D;
        }
    }

    public void OnEnterInterface(GameObject obj)
    {
        playerInventorySystem.mouseItem.ui = obj.GetComponent<UserInterface>();
    }

    public void OnExitInterface(GameObject obj)
    {
        playerInventorySystem.mouseItem.ui = null;
    }
}
public class MouseItem
{
    public UserInterface ui;
    public GameObject obj;
    public SlotObject item;
    public SlotObject hoverItem;
    public GameObject hoverObj;
}