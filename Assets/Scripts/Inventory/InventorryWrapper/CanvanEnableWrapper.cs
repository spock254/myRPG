using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvanEnableWrapper : MonoBehaviour
{
    public void EnableUI() 
    {
        this.enabled = !this.enabled;
    }
}
