using System.ComponentModel.Design.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
public class BuildingObject : MonoBehaviour
{
    public BuildingType buildingType;
    public float gridSize = 2;
    public Sprite sprite;
    public Behaviour[] components;
    public Component[] externalComponents;
    public UnityEvent deletionEvent;
    public bool canBePlacedOnSlope = false;
    public bool grounded = true;

    public Transform[] groundedPoints;


    public void EnableComponents()
    {         
        foreach (var component in components)
        {
            component.enabled = true;
        }
        foreach(var component in externalComponents)
        {
            (component as Behaviour).enabled = true;
        }

    }

    public void DeleteBuildingObject()
    {
        deletionEvent.Invoke();
        Destroy(gameObject);
    }

}

public enum BuildingType
{
    GroundBlock,
    Building,
    Vegetation
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(BuildingObject)), CanEditMultipleObjects]
public class BuildingObjectEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        BuildingObject buildingObject = (BuildingObject)target;

        if (GUILayout.Button("Assign Sprite"))
        {
            if(!AssignSprite(buildingObject))
            {
                Debug.LogError("Sprite not found for " + buildingObject.name);
            }
        }
    }
    

    private bool AssignSprite(BuildingObject target)
    {
        // Get sprite from Screenshot folder
        string path = "Assets/Screenshots/" + target.name + ".png";
        Sprite sprite = UnityEditor.AssetDatabase.LoadAssetAtPath<Sprite>(path);
        if(sprite == null)
        {
            Debug.LogError("Sprite not found at path: " + path);
            return false;
        }
        target.sprite = sprite;
        // Set Dirty
        EditorUtility.SetDirty(target);
        // Save Prefab
        PrefabUtility.SavePrefabAsset(target.gameObject);
        return true;
    }
}
#endif