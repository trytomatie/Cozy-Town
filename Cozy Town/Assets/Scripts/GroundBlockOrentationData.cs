using UnityEngine;

[CreateAssetMenu(fileName = "GroundBlockOrentationData", menuName = "GroundBlockOrentationData", order = 1)]
public class GroundBlockOrentationData : ScriptableObject
{
    public Pattern assignedPattern;
    public Mesh mesh;
    public Vector3 rotation;
}
