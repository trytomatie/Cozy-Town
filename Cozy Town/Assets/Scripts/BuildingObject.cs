using UnityEngine;
using UnityEngine.Events;
using static TMPro.TMP_InputField;

public class BuildingObject : MonoBehaviour
{
    public BuildingType buildingType;
    public float gridSize = 2;
    public Sprite sprite;
    public Behaviour[] components;
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
