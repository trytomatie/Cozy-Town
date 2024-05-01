using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    private PlayerInput playerInputMap;
    public NavMeshSurface navMeshSurface;

    public Texture2D defaultCursor;
    public Texture2D destroyCursor;

    // Singelton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerInputMap = new PlayerInput();
            playerInputMap.Enable();
        }
        else
        {
            Destroy(this);
        }
    }

    public static PlayerInput PlayerInputMap
    {         get
        {
            return instance.playerInputMap;
        }
    }

    public void BakeNavMeshData()
    {
        navMeshSurface.BuildNavMesh();

    }


}
