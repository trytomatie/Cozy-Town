using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;


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
            SetCursor(0);
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

    public static void SetCursor(int i)
    {
        switch (i)
        {
            case 0:
                Cursor.SetCursor(instance.defaultCursor, Vector2.zero, CursorMode.Auto);
                break;
            case 1:
                Cursor.SetCursor(instance.destroyCursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    public void BakeNavMeshData()
    {
        navMeshSurface.BuildNavMesh();

    }


}
