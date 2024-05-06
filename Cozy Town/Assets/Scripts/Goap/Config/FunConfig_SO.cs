using UnityEngine;

[CreateAssetMenu(fileName = "FunConfig", menuName = "Configs/FunConfig")]
public class FunConfig_SO : ScriptableObject
{
    public float sensorRadius = 10;
    public int energyCost = 25;
    public LayerMask sensorMask;
}
