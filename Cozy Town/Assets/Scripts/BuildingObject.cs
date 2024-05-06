using System.ComponentModel.Design.Serialization;
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
