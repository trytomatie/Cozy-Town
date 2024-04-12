using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject buildingIndictaor;
    public LayerMask groundLayer;
    public LayerMask placeLayer;

    public GroundBlockOrentationData[] groundBlockOrentationDataList;

    public Material canPlaceMaterial;
    public Material cantPlaceMaterial;
    public bool placeBuildingMode = false;
    private int selectedBuildingIndex = 0;
    private Vector3 gridOffset = new Vector3(0f,0, 0f);
    // Singleton
    public static BuildingManager instance;

    // Flags
    private bool lockPlaceInput = true;

    public GameObject cornerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        SetBuildingIndicator(0);
        PlaceBuildingMode = true;
    }

    private void FixedUpdate()
    {
        if (PlaceBuildingMode)
        {
            GetWorldPositionPointer();
        }
    }

    public void SetBuildingIndicator(int index)
    {
        if(buildingPrefabs.Length > index)
        {
            selectedBuildingIndex = index;
            if(buildingIndictaor.transform.GetChild(0).childCount > 0)
            {
                Destroy(buildingIndictaor.transform.GetChild(0).GetChild(0).gameObject);
            }

            Instantiate(buildingPrefabs[selectedBuildingIndex], buildingIndictaor.transform.position, buildingIndictaor.transform.rotation, buildingIndictaor.transform.GetChild(0));
            // Disable all Collider
            Collider[] colliders = buildingIndictaor.GetComponentsInChildren<Collider>();
            foreach(Collider c in colliders)
            {
                c.enabled = false;
            }
            buildingIndictaor.SetActive(false);
        }
    }

    public void GetWorldPositionPointer()
    {
        // Check If Mouse is over UI
        if (EventSystem.current.IsPointerOverGameObject())
        {
            buildingIndictaor.SetActive(false);
            return;
        }
        else
        {
            buildingIndictaor.SetActive(true);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,20, groundLayer) /*&& hit.normal == Vector3.up*/)
        {
            Vector3 position = ray.GetPoint(hit.distance-0.1f) - gridOffset;
            PlaceBuildingIndicator(position);
        }
        else
        {
            buildingIndictaor.SetActive(false);
        }
    }

    public void PlaceBuilding()
    {
        if(CanPlaceBuilding(buildingIndictaor.transform.position) && !lockPlaceInput)
        {
            Instantiate(buildingPrefabs[selectedBuildingIndex], buildingIndictaor.transform.position, buildingIndictaor.transform.rotation);
        }
    }

    public void PlaceBuildingIndicator(Vector3 pos)
    {
        // Round Position to nearest even Number
        pos = new Vector3(Mathf.Round(pos.x / 2) * 2, Mathf.Round(pos.y/2 ) * 2, Mathf.Round(pos.z/2) * 2);

        buildingIndictaor.transform.position = pos + gridOffset;
        if (CanPlaceBuilding(pos + gridOffset))
        {
            SetMaterial(buildingIndictaor, canPlaceMaterial);
        }
        else
        {
            SetMaterial(buildingIndictaor, cantPlaceMaterial);
        }
    }

    private void SetMaterial(GameObject go, Material material)
    {
        MeshRenderer[] meshRenderers = go.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mr in meshRenderers)
        {
            if (mr.gameObject.layer == 1) continue;
            mr.material = material;
        }
    }

    private bool CanPlaceBuilding(Vector3 pos)
    {
        Collider[] colliders = Physics.OverlapBox(pos,new Vector3(0.45f,0.45f,0.45f), Quaternion.identity, placeLayer);
        if (colliders.Length > 0)
        {
            return false;
        }
        return true;
    }

    public void RotateBuilding()
    {
        buildingIndictaor.transform.Rotate(Vector3.up, 90);
    }

    private void UnlockPlacementInput()
    {
        lockPlaceInput = false;
    }

    public bool PlaceBuildingMode 
    { get => placeBuildingMode;
        set 
        {
            placeBuildingMode = value;
            if(value)
            {
                //GameUI.instance.interfaceAnimator.SetFloat("Buildingmode", 1);
                buildingIndictaor.SetActive(true);
                print("Building Mode Activated");
                GameManager.PlayerInputMap.Player.RotateBuilding.performed += ctx => RotateBuilding();
                GameManager.PlayerInputMap.Player.PlaceBuilding.performed += ctx => PlaceBuilding();
                Invoke("UnlockPlacementInput", 0.25f);
                
            }
            else
            {
                //GameUI.instance.interfaceAnimator.SetFloat("Buildingmode", 0);
                buildingIndictaor.SetActive(false);
                GameManager.PlayerInputMap.Player.RotateBuilding.performed -= ctx => RotateBuilding();
                GameManager.PlayerInputMap.Player.PlaceBuilding.performed -= ctx => PlaceBuilding();
                lockPlaceInput = true;
            }
        }
    }
}

// Editor Script
#if UNITY_EDITOR
[CustomEditor(typeof(BuildingManager))]
public class BuildingManagerEditor : Editor
{
    int selectedBuildingIndex = 0;
    public override void OnInspectorGUI()
    {
        BuildingManager buildingManager = (BuildingManager)target;
        // Draw Default Inspector
        DrawDefaultInspector();
        // Cycle through selectedBuildingIndex
        selectedBuildingIndex = EditorGUILayout.IntField("Selected Building Index", selectedBuildingIndex);
        if (GUILayout.Button("Set Building Indicator"))
        {
            buildingManager.SetBuildingIndicator(selectedBuildingIndex);
        }

        // Show Hint if not all Patterns are Assigned
        foreach(Pattern pattern in Enum.GetValues(typeof(Pattern)))
        {
            if(buildingManager.groundBlockOrentationDataList.Where(x => x.assignedPattern == pattern).Count() == 0)
            {
                EditorGUILayout.HelpBox($"No Pattern assigned for {pattern}", MessageType.Warning);
            }
        }
    }
}   
#endif
