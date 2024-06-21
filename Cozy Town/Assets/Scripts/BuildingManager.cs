using FronkonGames.Artistic.OilPaint.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public GameObject buildingIndictaor;
    public GameObject outlineIndicator;
    public LayerMask groundLayer;
    public LayerMask placeLayer;

    public GroundBlockOrentationData[] groundBlockOrentationDataList;
    public GroundBlockOrentationData[] jettyBlockOrentationDataList;
    public GroundBlockOrentationData[] pathBlockOrientationDataList;
    public Material canPlaceMaterial;
    public Material cantPlaceMaterial;
    public Material deleteMaterial;
    public Material hoveredOutlineMaterial;
    public bool placeBuildingMode = false;
    private int selectedBuildingIndex = 0;
    private EditingMode buildingEditingMode = EditingMode.Edit;
    private GameObject deletionTarget;
    private GameObject hoveredTaget;
    private Material[] deletionTargetOriginalMaterialRef;

    private Vector3 gridOffset = new Vector3(0f,0, 0f);
    // Singleton
    public static BuildingManager instance;

    // Flags
    private bool lockPlaceInput = true;
    private bool placeingButtonRegistered = false;

    public GameObject cornerPrefab;

    [Header("Fences")]
    public GameObject fenceHorizontal;
    public GameObject fenceEnd;

    public enum EditingMode
    {
        Edit,
        Delete,
        
    }

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


    }

    private void FixedUpdate()
    {
        if (PlaceBuildingMode)
        {
            GetWorldPositionPointer();
            return;
        }
        if(BuildingDeletionMode != EditingMode.Edit)
        {
            DeletionTarget = GetHoveredObject();
        }
        else
        {
            HoveredTaget = GetHoveredObject();
        }

    }

    private void ExitBuildingMode()
    {
        print("HEHEHYHYHYH");
        PlaceBuildingMode = false;
    }

    private void Update()
    {
        PlaceOrRemoveBuilding();
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
            // Reset Rotation if GroundBlock
            if (buildingIndictaor.GetComponentInChildren<BuildingObject>().buildingType == BuildingType.GroundBlock)
            {
                buildingIndictaor.transform.eulerAngles = new Vector3(0, 0, 0);
            }
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
        BuildingObject bo = buildingPrefabs[selectedBuildingIndex].GetComponent<BuildingObject>();
        if (Physics.Raycast(ray, out hit,60, groundLayer) /*&& hit.normal == Vector3.up*/)
        {
            Vector3 position;
            if (bo.gridSize == 2)
            {
                position = hit.collider.transform.position + (hit.normal * 2);
            }
            else
            {
                position = hit.point + gridOffset;
            }
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
            {
                position = hit.point + gridOffset;
                PlaceBuildingIndicator(position, bo.gridSize);
                return;
            }

            PlaceBuildingIndicator(position, bo.gridSize);
        }
        else
        {
            buildingIndictaor.SetActive(false);
        }
    }

    public void PlaceOrRemoveBuilding()
    {
        if (!placeingButtonRegistered) return;
        placeingButtonRegistered = false;
        if(BuildingDeletionMode == EditingMode.Delete)
        {
            if(DeletionTarget != null && !EventSystem.current.IsPointerOverGameObject())
            {
                SoundManager.PlaySound(1, DeletionTarget.transform.position);
                DeletionTarget.GetComponent<BuildingObject>().DeleteBuildingObject();
                DeletionTarget = null;
                GameManager.instance.BakeNavMeshDataNextFrame();
            }
        }
        else
        {
            if (CanPlaceBuilding(buildingIndictaor.transform.position) && !lockPlaceInput && buildingIndictaor.activeSelf)
            {
                GameObject go = Instantiate(buildingPrefabs[selectedBuildingIndex], buildingIndictaor.transform.position, buildingIndictaor.transform.rotation);
                go.GetComponent<BuildingObject>().EnableComponents();
                SoundManager.PlaySound(0, go.transform.position);
                GameManager.instance.BakeNavMeshData();
            }
        }


    }

    public void PlaceBuildingIndicator(Vector3 pos,float gridSize)
    {
        // Round Position to nearest even Number
        pos = new Vector3(Mathf.Round(pos.x / gridSize) * gridSize, Mathf.Round(pos.y/ gridSize) * gridSize, Mathf.Round(pos.z/ gridSize) * gridSize);

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

    public GameObject GetHoveredObject()
    {
        buildingIndictaor.SetActive(false);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return null;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 60) /*&& hit.normal == Vector3.up*/)
        {
            if(hit.collider.GetComponent<BuildingObject>() != null)
            {
                return hit.collider.gameObject;
            }
        }
            return null;
    }



    private bool CanPlaceBuilding(Vector3 pos)
    {
        Collider[] colliders;

        BuildingObject bo = buildingPrefabs[selectedBuildingIndex].GetComponent<BuildingObject>();
        BoxCollider col = bo.GetComponent<BoxCollider>();
        if (col != null)
        {
            colliders = Physics.OverlapBox(pos + col.center, col.size / 2, Quaternion.identity, placeLayer);
            if (colliders.Length > 0)
            {
                return false;
            }
        }
        else
        {
            colliders = Physics.OverlapBox(pos + new Vector3(0, 0.5f, 0), new Vector3(0.45f, 0.45f, 0.45f), Quaternion.identity, placeLayer);
            if (colliders.Length > 0)
            {
                print("Colliding , wo hitbox?");
                return false;
            }
        }
        if(bo.grounded)
        {
            RaycastHit hit;
            Transform[] groundedPoints = buildingIndictaor.GetComponentInChildren<BuildingObject>().groundedPoints;
            if (groundedPoints.Length == 0)
            {
                groundedPoints = new Transform[1] { buildingIndictaor.transform };
            }
            int i = 0;
            foreach (Transform t in groundedPoints)
            {
                if (Physics.Raycast(t.position + new Vector3(0,0.1f,0), Vector3.down, out hit, 0.12f, groundLayer))
                {
                    if(hit.collider.gameObject.layer == LayerMask.NameToLayer("Water"))
                    {
                        return false;
                    }
                    if(!bo.canBePlacedOnSlope)
                    {
                        if (hit.normal != Vector3.up)
                        {
                            print("OnSlope");
                            return false;
                        }
                    }
                }
                else
                {
                    print("NotGrounded");
                    return false;
                }
                i++;
            }

        }
        return true;
    }

    public void RotateBuilding()
    {
        if(buildingIndictaor.GetComponentInChildren<BuildingObject>().buildingType == BuildingType.GroundBlock)
        {
            if(buildingIndictaor.GetComponentInChildren<BuildingObject>().gameObject.tag.Equals("ForceRotate"))
            {
                buildingIndictaor.transform.Rotate(Vector3.up, 90);
            }
            else
            {
                buildingIndictaor.transform.eulerAngles = new Vector3(0, 0, 0);
                return;
            }

        }
        else
        {
            buildingIndictaor.transform.Rotate(Vector3.up, 45);
        }
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
                BuildingDeletionMode = EditingMode.Edit;
                //GameUI.instance.interfaceAnimator.SetFloat("Buildingmode", 1);
                buildingIndictaor.SetActive(true);
                print("Building Mode Activated");
                GameManager.PlayerInputMap.Player.RotateBuilding.performed += ctx => RotateBuilding();
                GameManager.PlayerInputMap.Player.PlaceBuilding.performed += ctx => placeingButtonRegistered = true;
                GameManager.PlayerInputMap.Player.ExitCurrentMode.performed += ctx => ExitBuildingMode();
                Invoke("UnlockPlacementInput", 0.25f);
                
            }
            else
            {
                //GameUI.instance.interfaceAnimator.SetFloat("Buildingmode", 0);
                buildingIndictaor.SetActive(false);
                GameManager.PlayerInputMap.Player.RotateBuilding.performed -= ctx => RotateBuilding();
                GameManager.PlayerInputMap.Player.PlaceBuilding.performed -= ctx => placeingButtonRegistered = true;
                GameManager.PlayerInputMap.Player.ExitCurrentMode.performed -= ctx => ExitBuildingMode();
                lockPlaceInput = true;
            }
        }
    }

    public void EnableDeletionMode()
    {
        BuildingDeletionMode = EditingMode.Delete;
    }

    public EditingMode BuildingDeletionMode 
    { 
        get => buildingEditingMode;
        set 
        {
            if(value != EditingMode.Edit)
            {
                GameManager.SetCursor(1);
                PlaceBuildingMode = false;
            }
            else
            {
                GameManager.SetCursor(0);
                DeletionTarget = null;
            }
            buildingEditingMode = value;
            
        } 
    }

    public GameObject DeletionTarget { 
        get => deletionTarget;
        set
        {
            if(value != deletionTarget)
            {
                if(deletionTarget != null)
                {
                    MeshRenderer[] meshRenderers = deletionTarget.GetComponentsInChildren<MeshRenderer>();

                    int i = 0;
                    foreach (MeshRenderer mr in meshRenderers)
                    {
                        mr.material = deletionTargetOriginalMaterialRef[i];
                        i++;
                    }
                }
                if(value != null)
                {
                    MeshRenderer[] meshRenderers = value.GetComponentsInChildren<MeshRenderer>();
                    deletionTargetOriginalMaterialRef = new Material[meshRenderers.Length];
                    int i = 0;
                    foreach (MeshRenderer mr in meshRenderers)
                    {
                        deletionTargetOriginalMaterialRef[i] = mr.material;
                        mr.material = deleteMaterial;
                        i++;
                    }
                }
                deletionTarget = value;
            }

        } 
    }

    public GameObject HoveredTaget { get => hoveredTaget; set
        {
            if (value != hoveredTaget)
            {
                if (value == null)
                {
                    outlineIndicator.SetActive(false);
                    return;
                }
                if (value != null)
                {
                    BuildingObject bo = value.GetComponent<BuildingObject>();
                    if (bo != null)
                    {
                        if (bo.buildingType != BuildingType.GroundBlock)
                        {
                            if (outlineIndicator.transform.childCount > 0) Destroy(outlineIndicator.transform.GetChild(0).gameObject);
                            GameObject _Instance = Instantiate(value, outlineIndicator.transform.position, value.transform.rotation, outlineIndicator.transform);
                            _Instance.GetComponent<BuildingObject>().DisableComponents();
                            foreach (Renderer renderer in _Instance.GetComponentsInChildren<Renderer>())
                            {
                                renderer.material = hoveredOutlineMaterial;
                            }
                            outlineIndicator.SetActive(true);
                        }
                        else
                        {
                            if (outlineIndicator.transform.childCount > 0) Destroy(outlineIndicator.transform.GetChild(0).gameObject);
                            outlineIndicator.SetActive(false);
                        }
                    }

                    
                    outlineIndicator.transform.position = value.transform.position;
                }
                hoveredTaget = value;
            }

        }
    }

    public void PlaceOutline(GameObject go)
    {
        if (go == null)
        {

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

        // Show hint if a BuildingPrefab has no BuildingObject Component
        for(int i = 0; i < buildingManager.buildingPrefabs.Length; i++)
        {
            if (buildingManager.buildingPrefabs[i].GetComponent<BuildingObject>() == null)
            {
                EditorGUILayout.HelpBox($"BuildingPrefab at Index {i} has no BuildingObject Component", MessageType.Warning);
            }
        }
    }
}   
#endif
