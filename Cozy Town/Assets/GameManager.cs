using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    private PlayerInput playerInputMap;


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


}
