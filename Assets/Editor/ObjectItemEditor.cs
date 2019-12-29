using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectItem))]
public class ObjectItemEditor : Editor
{
    ObjectItem objectItem;

    private void OnEnable()
    {
        objectItem = (ObjectItem)target;
    }
    public override void OnInspectorGUI()
    {
        int width = EditorGUILayout.IntField("Map Sprite Width", objectItem.id);

        EditorUtility.SetDirty(objectItem);
    }
}
