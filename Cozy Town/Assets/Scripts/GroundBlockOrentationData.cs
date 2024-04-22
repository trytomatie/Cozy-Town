﻿using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using NUnit.Framework.Constraints;

[CreateAssetMenu(fileName = "GroundBlockOrentationData", menuName = "GroundBlockOrentationData", order = 1)]
public class GroundBlockOrentationData : SerializedScriptableObject
{
    [TableMatrix(HorizontalTitle = "Custom Cell Drawing", DrawElementMethod = "DrawColoredEnumElement", ResizableColumns = false, RowHeight = 16,SquareCells = true)]
    public int[,] CustomCellDrawing = new int[3,3];

    private static int DrawColoredEnumElement(Rect rect, int value)
    {
        if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
        {
            switch(value)
            {
                case 0:
                    value = 1;
                    break;
                case 1:
                    value = 8;
                break;
                case 8:
                    value = 0;
                break;
            }
            GUI.changed = true;
            Event.current.Use();
        }
        switch(value)
        {
            case 0:
                UnityEditor.EditorGUI.DrawRect(rect.Padding(1), new Color(0, 0, 0, 0.5f));
                break;
            case 1:
                UnityEditor.EditorGUI.DrawRect(rect.Padding(1), new Color(0.1f, 0.8f, 0.2f));
                break;
            case 8:
                UnityEditor.EditorGUI.DrawRect(rect.Padding(1), Color.cyan);
                break;
        }

        return value;
    }
    public Pattern assignedPattern;
    public Mesh mesh;
    public Vector3 rotation;
}
