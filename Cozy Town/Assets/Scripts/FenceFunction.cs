using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class FenceFunction : MonoBehaviour
{
    public static Dictionary<Vector3Int, FenceFunction> fences = new Dictionary<Vector3Int, FenceFunction>();

    public void Start()
    {
        Vector3Int pos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        if(!fences.ContainsKey(pos))
        {
            fences.Add(pos, this);
            CheckForFenceConntection(pos);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CheckForFenceConntection(Vector3Int pos)
    {
        Vector3Int[] directions = new Vector3Int[]
        {
            new Vector3Int(0,0,2),
            new Vector3Int(0,0,-2),
            new Vector3Int(-2,0,0),
            new Vector3Int(2,0,0)
        };

        foreach (var direction in directions)
        {
            Vector3Int newPos = pos + direction;
            if(fences.ContainsKey(newPos))
            {
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                if (direction == new Vector3Int(0,0,2) || direction == new Vector3Int(0,0,-2))
                {
                    rotation = Quaternion.identity;
                }
                GameObject connection = Instantiate(BuildingManager.instance.fenceHorizontal, direction / 2 + pos, rotation);
                connection.GetComponent<FenceConnection>().connections.Add(this);
                connection.GetComponent<FenceConnection>().connections.Add(fences[newPos]);
            }
        }
    }
}
