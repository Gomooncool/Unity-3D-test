using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float size = 0.1f;
    public GameObject Field;
    public Vector3[,] ArrayofGridPositions;
    public Vector3 AllignedPoint;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }

    

    public void OnDrawGizmos()
    {   
        Collider Col = Field.GetComponent<Collider>();
        Gizmos.color = Color.yellow;
        //Debug.Log(Col.bounds.size.x);
        //Debug.Log(Col.bounds.size.z);
        ArrayofGridPositions = new Vector3[SetGridPosArraySizeX(Col)+1, SetGridPosArraySizeZ(Col)+1];
        Debug.Log(SetGridPosArraySizeX(Col));
        Debug.Log(SetGridPosArraySizeZ(Col));
        int Xi;
        Xi = 0;
        int Ji;
        Ji = 0;
        for (float x = 0; x <= Col.bounds.size.x; x += size)
        {
            Ji = 0;
            for (float z = 0; z <= Col.bounds.size.z; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                AllignedPoint = new Vector3(point.x - Col.bounds.extents.x + Field.transform.position.x, point.y, point.z - Col.bounds.extents.z + Field.transform.position.z);
                Gizmos.DrawSphere(AllignedPoint, 0.02f);
                ArrayofGridPositions[Xi,Ji] = AllignedPoint;
                Ji++;
            }
            Xi++;
        }


        int SetGridPosArraySizeX(Collider Col1)
        {
                int SizeX = Mathf.RoundToInt(Col1.bounds.size.x / size);
                return SizeX;
           
        }

        int SetGridPosArraySizeZ(Collider Col1)
        {
            int SizeZ = Mathf.RoundToInt(Col1.bounds.size.z / size);
            return SizeZ;

        }

    }
}
