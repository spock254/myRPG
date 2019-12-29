using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCStats : MonoBehaviour
{
    [SerializeField]
    [Range(0, 15)]
    float moveSpeed;
    public float MoveSpeed
    {
        get { return this.moveSpeed; }
        set { if (value >= 0) { this.moveSpeed = value; } }
    }
}
