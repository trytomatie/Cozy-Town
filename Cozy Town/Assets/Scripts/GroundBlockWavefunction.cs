using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.UI.GridLayoutGroup;

public class GroundBlockWavefunction : MonoBehaviour
{
    public BlockType blockType;
    public Pattern debugPattern;
    public bool ramp;
    public int debugDifference;
    public static Dictionary<Vector3Int, GroundBlockWavefunction> groundBlockMatrix = new Dictionary<Vector3Int, GroundBlockWavefunction>();
    public static Dictionary<Vector3Int, GroundBlockWavefunction> pathWayMatrix = new Dictionary<Vector3Int, GroundBlockWavefunction>();
    public static Dictionary<Vector3Int, GroundBlockWavefunction> jettyBlockMatrix = new Dictionary<Vector3Int, GroundBlockWavefunction>();
    public static Dictionary<Vector3Int, CornerFunction> cornerMatrix = new Dictionary<Vector3Int, CornerFunction>();

    private MaterialPropertyBlock materialPropertyBlock;

    private void Start()
    {
        Dictionary<Vector3Int, GroundBlockWavefunction> matrix = null;
        switch (blockType)
        {
            case BlockType.Ground:
                matrix = groundBlockMatrix;
                break;
            case BlockType.Ramp:
                matrix = groundBlockMatrix;
                break;
            case BlockType.Jetty:
                matrix = jettyBlockMatrix;
                break;
            case BlockType.Path:
                matrix = pathWayMatrix;
                materialPropertyBlock = new MaterialPropertyBlock();
                break;
        }
        try
        {
            matrix.Add(new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z), this);
        }
        catch(Exception e)
        {
            Destroy(gameObject);
            return;
        }

        if(!ramp)
        {
            UpdateBlock(true, matrix);
        }
        else
        {
            UpdateSurroundingBlocks(new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z), matrix);
        }

    }


    public void UpdateBlock(bool mainUpdate,Dictionary<Vector3Int,GroundBlockWavefunction> matrix)
    {
        if(ramp)
        {
            return;
        }
        Vector3Int myPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y,(int)transform.position.z);
        int[] waveFunction = new int[9];
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                if(matrix.ContainsKey(new Vector3Int(myPosition.x + (x-1)*2, myPosition.y, myPosition.z + (y-1)*2)))
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
        GroundBlockOrentationData[] dataList = null;
        switch(blockType)
        {
            case BlockType.Ground:
                dataList = BuildingManager.instance.groundBlockOrentationDataList;
                break;
            case BlockType.Ramp:
                break;
            case BlockType.Jetty:
                dataList = BuildingManager.instance.jettyBlockOrentationDataList;
                break;
            case BlockType.Path:
                dataList = BuildingManager.instance.pathBlockOrientationDataList;
                break;
        }
        Pattern pattern = CheckWaveFunction(waveFunction, dataList);
        debugPattern = pattern;

        foreach(GroundBlockOrentationData data in dataList)
        {
            if(data.assignedPattern == pattern)
            {
                if(data.blockType == BlockType.Path)
                {
                    transform.rotation = Quaternion.Euler(data.rotation);
                    materialPropertyBlock.SetVector("_BaseMap_ST", new Vector4(0.25f, 0.25f, data.pathOffset.x, data.pathOffset.y));
                    //GetComponent<DecalProjector>().uvBias = data.pathOffset;

                    foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>(true))
                    {
                        mr.enabled = true;
                        mr.SetPropertyBlock(materialPropertyBlock);
                    }
                    GetComponentsInChildren<MeshRenderer>().Where(x => x.gameObject.name == "Indicator").First().enabled = false;
                }
                else
                {
                    GetComponent<MeshFilter>().mesh = data.mesh;
                    transform.rotation = Quaternion.Euler(data.rotation);
                }

                break;
            }
        }
        if(mainUpdate)
        {
            UpdateSurroundingBlocks(myPosition,matrix);
            CornerCheck();
        }
    }

    public void RemoveBlock()
    {
        Vector3Int myPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        switch(blockType)
        {
            case BlockType.Ground:
                groundBlockMatrix.Remove(myPosition);
                UpdateSurroundingBlocks(myPosition, groundBlockMatrix);
                break;
            case BlockType.Ramp:
                groundBlockMatrix.Remove(myPosition);
                break;
            case BlockType.Jetty:
                break;
            case BlockType.Path:
                pathWayMatrix.Remove(myPosition);
                UpdateSurroundingBlocks(myPosition, pathWayMatrix);
                break;
        }

    }

    private static void UpdateSurroundingBlocks(Vector3Int myPosition, Dictionary<Vector3Int, GroundBlockWavefunction> matrix)
    {
        // Update all block around this block
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                if (matrix.ContainsKey(new Vector3Int(myPosition.x + x * 2, myPosition.y, myPosition.z + y * 2)))
                {
                    matrix[new Vector3Int(myPosition.x + x * 2, myPosition.y, myPosition.z + y * 2)].UpdateBlock(false, matrix);
                }
            }
        }
    }

    private static readonly int[] _0DEGREE_CORNER = new int[] { 1, 1, 8,
                                                                1, 0, 8,
                                                                8, 8, 8};
    private static readonly int[] _90DEGREE_CORNER = new int[] { 8, 1, 1,
                                                                 8, 0, 1,
                                                                 8, 8, 8};
    private static readonly int[] _180DEGREE_CORNER = new int[] { 8, 8, 8,
                                                                  8, 0, 1,
                                                                  8, 1, 1};
    private static readonly int[] _270DEGREE_CORNER = new int[] { 8, 8, 8,
                                                                  1, 0, 8,
                                                                  1, 1, 8};



    public void CornerCheck()
    {
        List<Vector3Int> emptyPostions = new List<Vector3Int>();
        Vector3Int myPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {

                Vector3Int pos = new Vector3Int(myPosition.x + x * 2, myPosition.y, myPosition.z + y * 2);
                Debug.DrawLine(pos, pos+ new Vector3(0,5,0), Color.red, 6);
                if (!groundBlockMatrix.ContainsKey(pos))
                {
                    emptyPostions.Add(pos);
                }
                else if(cornerMatrix.ContainsKey(pos))
                {
                    cornerMatrix[pos].DestroyCorner();
                    cornerMatrix.Remove(pos);
                }
            }
        }

        List<int[]> patterns = new List<int[]>
        {
            _0DEGREE_CORNER,
            _90DEGREE_CORNER,
            _180DEGREE_CORNER,
            _270DEGREE_CORNER
        };
        foreach(Vector3Int pos in emptyPostions)
        {
            int[] waveFunction = new int[9];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Vector3Int vec = new Vector3Int(pos.x + (x - 1) * 2, pos.y, pos.z + (y - 1) * 2);
                    if (groundBlockMatrix.ContainsKey(vec))
                    {
                        if (!groundBlockMatrix[vec].ramp)
                        {
                            waveFunction[x * 3 + y] = 1;
                        }

                    }
                    else
                    {
                        waveFunction[x * 3 + y] = 0;
                    }
                }
            }
            bool cornerFound = false;
            for(int i = 0; i < patterns.Count; i++)
            {
                if(CalculateDifference(waveFunction, patterns[i]) == 0)
                {
                    print(i);
                    if(!cornerFound)
                    {
                        if(!cornerMatrix.ContainsKey(pos))
                        {
                            GameObject corner = Instantiate(BuildingManager.instance.cornerPrefab, pos, Quaternion.identity);
                            cornerMatrix.Add(pos, corner.GetComponent<CornerFunction>());
                        }
                        cornerFound = true;
                    }
                    cornerMatrix[pos].SetCorner(i, true);
                    continue;
                }
            }
            if(!cornerFound)
            {
                if(cornerMatrix.ContainsKey(pos))
                {
                    cornerMatrix[pos].DestroyCorner();
                    cornerMatrix.Remove(pos);
                }
            }
        }
    }

    // 1 = block, 0 = no block 8 = ignore
    private static readonly int[] MIDDLE = new int[] {0, 0, 0,
                                                      0, 1, 0, 
                                                      0, 0, 0};
    private static readonly int[] BOTTOM = new int[] {8, 1, 8,
                                                      0, 1, 0,
                                                      0, 0, 0};
    private static readonly int[] TOP = new int[] {0, 0, 0,
                                                   0, 1, 0,
                                                   8, 1, 8};
    private static readonly int[] LEFT = new int[] {0, 0, 8,
                                                    0, 1, 1,
                                                    0, 0, 8};
    private static readonly int[] RIGHT = new int[] {8, 0, 0,
                                                    1, 1, 0,
                                                    8, 0, 0};
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

    //private static List<int[]> patterns = new List<int[]>
    //    {
    //        MIDDLE,
    //        BOTTOM,
    //        TOP,
    //        LEFT,
    //        RIGHT,
    //        MIDDLE_CLOSED,
    //        BOTTOM_CLOSED,
    //        TOP_CLOSED,
    //        LEFT_CLOSED,
    //        RIGHT_CLOSED,
    //        TOPRIGHT_CORNER,
    //        TOPLEFT_CORNER,
    //        BOTTOMRIGHT_CORNER,
    //        BOTTOMLEFT_CORNER,
    //        SOUTHNORTH_BRIDGE,
    //        WESTEAST_BRIDGE,
    //        CROSS,
    //        BOTTOM_T,
    //        TOP_T,
    //        LEFT_T,
    //        RIGHT_T
    //    };
    public Pattern CheckWaveFunction(int[] waveFunction,GroundBlockOrentationData[] dataList)
    {
        Pattern closestPattern = Pattern.MIDDLE;
        int bestDifference = 1000;

        foreach (GroundBlockOrentationData data in dataList)
        {
            int[] pattern = new int[9];
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    pattern[y * 3 + x] = data.CustomCellDrawing[x, y];
                }
            }
            int difference = CalculateDifference(waveFunction, pattern);
            if(difference < bestDifference)
            {
                debugDifference = difference;
                bestDifference = difference;
                closestPattern = data.assignedPattern;
                if (bestDifference == 0)
                {
                    return data.assignedPattern;
                }
            }
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
public enum BlockType { Ground, Ramp, Jetty, Path }

