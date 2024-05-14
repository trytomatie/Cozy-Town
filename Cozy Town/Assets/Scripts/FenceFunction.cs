using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FenceFunction : MonoBehaviour
{
    public static Dictionary<Vector3Int, FenceFunction> fences = new Dictionary<Vector3Int, FenceFunction>();
    public Dictionary<FenceConnection, Vector3Int> connections = new Dictionary<FenceConnection,Vector3Int>();
    public Dictionary<GameObject, Vector3Int> endPieces = new Dictionary<GameObject, Vector3Int>();
    public void Start()
    {
        Vector3Int pos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        if(!fences.ContainsKey(pos))
        {
            fences.Add(pos, this);
            CheckForFenceConntection(pos);
            // Check Neighbouring fence posts
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaceEndPiece()
    {
        foreach(var connection in connections)
        {
            if(!connections.Values.Contains(-connection.Value))
            {
                if(!endPieces.Keys.Contains(connection.Key.gameObject))
                {
                    float angle = ConvertDirectionToAngle(-connection.Value);
                    GameObject go = Instantiate(BuildingManager.instance.fenceEnd, transform.position, Quaternion.Euler(0, angle, 0));
                    endPieces.Add(go,-connection.Value);
                }
            }
            else
            {
                if(endPieces.Values.Contains(-connection.Value))
                {
                    GameObject go = endPieces.FirstOrDefault(x => x.Value == -connection.Value).Key;
                    Destroy(go);
                    endPieces.Remove(go);
                }
            }
        }
    }

    private static float ConvertDirectionToAngle(Vector3Int connection)
    {
        // Convert direction to angle
        float angle = 0;
        if (connection == new Vector3Int(0, 0, 1))
        {
            angle = 0;
        }
        else if (connection == new Vector3Int(0, 0, -1))
        {
            angle = 180;
        }
        else if (connection == new Vector3Int(-1, 0, 0))
        {
            angle = -90;
        }
        else if (connection == new Vector3Int(1, 0, 0))
        {
            angle = 90;
        }

        return angle;
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
                connections.Add(connection.GetComponent<FenceConnection>(),direction);
                fences[newPos].connections.Add(connection.GetComponent<FenceConnection>(), -direction);
                fences[newPos].PlaceEndPiece();
            }
        }
        PlaceEndPiece();
    }

    public void DeleteFencePost()
    {
        Vector3Int pos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        foreach(var connection in connections)
        {
            if(connection.Key != null)
            {
                connection.Key.connections.Remove(this);
                connection.Key.CheckConnections();
            }

        }
        fences.Remove(pos);
    }
}
