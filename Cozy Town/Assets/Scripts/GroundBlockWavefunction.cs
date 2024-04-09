using System.Collections.Generic;
using UnityEngine;

public class GroundBlockWavefunction : MonoBehaviour
{
    public Pattern debugPattern;
    public static Dictionary<Vector3Int, GroundBlockWavefunction> groundBlockMatrix = new Dictionary<Vector3Int, GroundBlockWavefunction>();

    private void Start()
    {
        groundBlockMatrix.Add(new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z), this);
        UpdateBlock(true);
    }

    public void UpdateBlock(bool mainUpdate)
    {
        Vector3Int myPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y,(int)transform.position.z);
        int[] waveFunction = new int[9];
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                if(groundBlockMatrix.ContainsKey(new Vector3Int(myPosition.x + (x-1)*2, myPosition.y, myPosition.z + (y-1)*2)))
                {
                    waveFunction[x*3+y] = 1;
                }
                else
                {
                    waveFunction[x*3+y] = 0;
                }
            }
        }
        // Print wavefunction
        //for(int x = 0; x < 9; x+=3)
        //{
        //    Debug.Log($"{waveFunction[x]}{waveFunction[x+1]}{waveFunction[x+2]}") ;
        //}
        Pattern pattern = CheckWaveFunction(waveFunction);
        debugPattern = pattern;
        if (mainUpdate)
        {
            Debug.Log(pattern);
        }

        foreach(GroundBlockOrentationData data in BuildingManager.instance.groundBlockOrentationDataList)
        {
            if(data.assignedPattern == pattern)
            {
                GetComponent<MeshFilter>().mesh = data.mesh;
                transform.rotation = Quaternion.Euler(data.rotation);
                break;
            }
        }
        if(mainUpdate)
        {        
            // Update all block around this block
            for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    if(x == 0 && y == 0)
                    {
                        continue;
                    }
                    if(groundBlockMatrix.ContainsKey(new Vector3Int(myPosition.x + x*2, myPosition.y, myPosition.z + y*2)))
                    {
                        groundBlockMatrix[new Vector3Int(myPosition.x + x*2, myPosition.y, myPosition.z + y*2)].UpdateBlock(false);
                    }
                }
            }
        }


    }

    private static readonly int[] MIDDLE = new int[] {0, 0, 0,
                                                      0, 1, 0, 
                                                      0, 0, 0};
    private static readonly int[] BOTTOM = new int[] {0, 1, 0,
                                                      0, 1, 0,
                                                      0, 0, 0};
    private static readonly int[] TOP = new int[] {0, 0, 0,
                                                   0, 1, 0,
                                                   0, 1, 0};
    private static readonly int[] LEFT = new int[] {0, 0, 0,
                                                    0, 1, 1,
                                                    0, 0, 0};
    private static readonly int[] RIGHT = new int[] {0, 0, 0,
                                                    1, 1, 0,
                                                    0, 0, 0};
    private static readonly int[] MIDDLE_CLOSED = new int[] {1, 1, 1,
                                                            1, 1, 1,
                                                            1, 1, 1};
    private static readonly int[] BOTTOM_CLOSED = new int[] {8, 1, 8,
                                                             1, 1, 1,
                                                             8, 0, 8};
    private static readonly int[] TOP_CLOSED = new int[] {8, 0, 8,
                                                          1, 1, 1,
                                                          8, 1,8};
    private static readonly int[] LEFT_CLOSED = new int[] {8, 1, 8,
                                                           1, 1, 0,
                                                           8, 1, 8};
    private static readonly int[] RIGHT_CLOSED = new int[] {8, 1, 8,
                                                            0, 1, 1,
                                                            8, 1, 8};
    private static readonly int[] TOPRIGHT_CORNER = new int[] {8, 0, 0,
                                                               1, 1, 0,
                                                               8, 1, 8};
    private static readonly int[] TOPLEFT_CORNER = new int[] {0, 0, 8,
                                                              0, 1, 1,
                                                              8, 1, 8};
    private static readonly int[] BOTTOMRIGHT_CORNER = new int[] {8, 1, 8,
                                                                  1, 1, 0,
                                                                  8, 0, 0};
    private static readonly int[] BOTTOMLEFT_CORNER = new int[] {8, 1, 8,
                                                                0, 1, 1,
                                                                0, 0, 8};
    private static readonly int[] SOUTHNORTH_BRIDGE = new int[] {8, 1, 8,
                                                                 0, 1, 0,
                                                                 8, 1, 8};
    private static readonly int[] WESTEAST_BRIDGE = new int[] {8, 0, 8,
                                                               1, 1, 1,
                                                               8, 0, 8};
    private static readonly int[] CROSS = new int[] {0, 1, 0,
                                                    1, 1, 1,
                                                    0, 1, 0};
    private static readonly int[] BOTTOM_T = new int[] {0, 0, 0,
                                                        1, 1, 1,
                                                        0, 1, 0};
    private static readonly int[] TOP_T = new int[] {0, 1, 0,
                                                     1, 1, 1,
                                                     0, 0, 0};
    private static readonly int[] LEFT_T = new int[] {0, 1, 0,
                                                      0, 1, 1,
                                                      0, 1, 0};
    private static readonly int[] RIGHT_T = new int[] {0, 1, 0,
                                                       1, 1, 0,
                                                       0, 1, 0};
    private static List<int[]> patterns = new List<int[]>
        {
            MIDDLE,
            BOTTOM,
            TOP,
            LEFT,
            RIGHT,
            MIDDLE_CLOSED,
            BOTTOM_CLOSED,
            TOP_CLOSED,
            LEFT_CLOSED,
            RIGHT_CLOSED,
            TOPRIGHT_CORNER,
            TOPLEFT_CORNER,
            BOTTOMRIGHT_CORNER,
            BOTTOMLEFT_CORNER,
            SOUTHNORTH_BRIDGE,
            WESTEAST_BRIDGE,
            CROSS,
            BOTTOM_T,
            TOP_T,
            LEFT_T,
            RIGHT_T
        };
    public Pattern CheckWaveFunction(int[] waveFunction)
    {
        Pattern closestPattern = Pattern.MIDDLE;
        int bestDifference = 1000;

        int i = 0;
        foreach (int[] pattern in patterns)
        {
            int difference = CalculateDifference(waveFunction, pattern);
            if(difference < bestDifference)
            {
                bestDifference = difference;
                closestPattern = (Pattern)i;
                if (bestDifference == 0)
                {
                    return (Pattern)i;
                }
            }
            i++;
        }
        return closestPattern;
    }

    private int CalculateDifference(int[] waveFunction, int[] pattern)
    {
        int difference = 0;
        for(int i = 0; i < waveFunction.Length; i++)
        {
            int calculatedDifference = Mathf.Abs(waveFunction[i] - pattern[i]);
            if(calculatedDifference < 3)
            {
                difference += calculatedDifference;
            }

        }
        return difference;
    }

}

public enum Pattern
{
    MIDDLE = 0,
    BOTTOM = 1,
    TOP = 2,
    LEFT = 3,
    RIGHT = 4,
    MIDDLE_CLOSED = 5,
    BOTTOM_CLOSED = 6,
    TOP_CLOSED = 7,
    LEFT_CLOSED = 8,
    RIGHT_CLOSED = 9,
    TOPRIGHT_CORNER = 10,
    TOPLEFT_CORNER = 11,
    BOTTOMRIGHT_CORNER = 12,
    BOTTOMLEFT_CORNER = 13,
    SOUTHNORTH_BRIDGE = 14,
    WESTEAST_BRIDGE = 15,
    CROSS = 16,
    BOTTOM_T = 17,
    TOP_T = 18,
    LEFT_T = 19,
    RIGHT_T = 20
}


