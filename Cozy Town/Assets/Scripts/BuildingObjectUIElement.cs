using System;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class BuildingObjectUIElement : MonoBehaviourUIHoverElement
{
    public int index = -1;
    public Image image;

    internal void Setup(BuildingObject bo, int i)
    {
        image.sprite = bo.sprite;
        description = bo.gameObject.name;
        index = i;
        
    }

    public void SelectIndex()
    {
        if (index == -1)
        {
            Debug.LogError("Index not set");
            return;
        }
        BuildingManager.instance.SetBuildingIndicator(index);
        BuildingManager.instance.PlaceBuildingMode = true;
    }

}


