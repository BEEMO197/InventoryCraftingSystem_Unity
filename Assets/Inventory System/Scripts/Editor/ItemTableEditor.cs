using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemTable))]
public class ItemTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ItemTable itemTable = (ItemTable)target;
        if(itemTable)
        {
            if(GUILayout.Button("Assign item IDs"))
            {
                itemTable.AssignItemIDs();
            }
        }
    }
}
