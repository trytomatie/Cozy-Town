using UnityEngine;

public class BuildingObject : MonoBehaviour
{
    public BuildingType buildingType;
    public float gridSize = 2;
    public Sprite sprite;
    public Behaviour[] components;


    public void EnableComponents()
    {         
        foreach (var component in components)
        {
            component.enabled = true;
        }
    }
}

public enum BuildingType
{
    GroundBlock,
    Building,
    Vegetation
}
