using System.Collections.Generic;
using UnityEngine;

public class FenceConnection : MonoBehaviour
{
    public List<FenceFunction> connections = new List<FenceFunction>();

    public void CheckConnections()
    {
        if(connections.Count < 2)
        {
            Destroy(gameObject);
        }
    }
}
