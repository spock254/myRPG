using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryInputManager : MonoBehaviour
{
    [SerializeField]
    UnityEvent OnTabPressEvent;
    
    [SerializeField]
    UnityEventBool OnTabPressEventBool;
    // Start is called before the first frame update
    void Start()
    {
        if (OnTabPressEvent == null)
        {
            OnTabPressEvent = new UnityEvent();
        }

        if (OnTabPressEventBool == null)
        {
            OnTabPressEventBool = new UnityEventBool();
        }

        //add listeners
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnTabPressEvent.Invoke();

            OnTabPressEventBool.Invoke(OnTabPressEventBool.flag);
            OnTabPressEventBool.flag = !OnTabPressEventBool.flag;
        }
    }
}
[System.Serializable]
public class UnityEventBool : UnityEvent<bool>
{
    public bool flag = true;
}