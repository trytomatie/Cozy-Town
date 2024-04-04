using System.Collections;
using UnityEngine;


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
