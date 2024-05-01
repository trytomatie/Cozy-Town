using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class FenceFunction : MonoBehaviour
{
    public static Dictionary<Vector3Int, FenceFunction> fences = new Dictionary<Vector3Int, FenceFunction>();
    public List<FenceConnection> connections = new List<FenceConnection>();

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
            new Vector3Int(0,0,1),
            new Vector3Int(0,0,-1),
            new Vector3Int(-1,0,0),
            new Vector3Int(1,0,0)
        };

        foreach (var direction in directions)
        {
            Vector3Int newPos = pos + direction;

            if(fences.ContainsKey(newPos))
            {
                Quaternion rotation = Quaternion.Euler(0, 90, 0);
                if (direction == new Vector3Int(0,0,1) || direction == new Vector3Int(0,0,-1))
                {
                    rotation = Quaternion.identity;
                }
                // Get middle point between newPos and pos
                Vector3 connectionPos = (new Vector3(newPos.x, newPos.y, newPos.z) + new Vector3(pos.x, pos.y, pos.z)) / 2;
                GameObject connection = Instantiate(BuildingManager.instance.fenceHorizontal, connectionPos, rotation);
                connection.GetComponent<FenceConnection>().connections.Add(this);
                connection.GetComponent<FenceConnection>().connections.Add(fences[newPos]);
                connections.Add(connection.GetComponent<FenceConnection>());
                fences[newPos].connections.Add(connection.GetComponent<FenceConnection>());
            }
        }
    }

    public void DeleteFencePost()
    {
        Vector3Int pos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        foreach(var connection in connections)
        {
            connection.connections.Remove(this);
            connection.CheckConnections();
        }
        fences.Remove(pos);
    }
}
