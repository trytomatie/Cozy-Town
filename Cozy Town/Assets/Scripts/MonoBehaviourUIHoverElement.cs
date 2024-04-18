using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


public class MonoBehaviourUIHoverElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public string description = "-";

    public void OnPointerEnter(PointerEventData eventData)
    {
        DescriptorUI.instance.SetDescriptor(description);
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptorUI.instance.HideDescriptor();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        DescriptorUI.instance.transform.position = Input.mousePosition;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(MonoBehaviourUIHoverElement))]
public class MonoBehaviourUIHoverElementEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("This script will show a description when hovered over");
        MonoBehaviourUIHoverElement script = (MonoBehaviourUIHoverElement)target;
        if(script.description == "-")
        {
            script.description = script.gameObject.name;
            EditorUtility.SetDirty(script);
        }
        else
        {
            GUILayout.Label("Description: " + script.description);
        }
    }
}
#endif
