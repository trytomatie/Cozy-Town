using UnityEngine;

public class CornerFunction : MonoBehaviour
{
    bool[] corners = new bool[4];

    public GameObject[] cornerObjects = new GameObject[4];

    public void DestroyCorner()
    {
        Destroy(gameObject);
    }

    public void SetCorner(int index, bool value)
    {
        corners[index] = value;
        cornerObjects[index].SetActive(value);
    }

    
}


