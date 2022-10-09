using UnityEngine;
using System.Collections.Generic;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class GridMesh : MonoBehaviour
{
    public int GridSizeX;
    public int GridSizeZ;
    private Vector3 FieldOffset;
    private float ScaleCellCoeffX;
    private float ScaleCellCoeffZ;
    public Vector3[] GridVerticiesArray;
    public Vector3[,] GridVerticiesArrayFinal;

    void Awake()
    {
        ScaleCellCoeffX = gameObject.GetComponent<Collider>().bounds.extents.x *2  / GridSizeX / gameObject.transform.localScale.x;
        ScaleCellCoeffZ = gameObject.GetComponent<Collider>().bounds.extents.z *2 / GridSizeZ / gameObject.transform.localScale.z;

        FieldOffset = new Vector3(-gameObject.GetComponent<Collider>().bounds.extents.x / gameObject.transform.localScale.x,
        -gameObject.GetComponent<Collider>().bounds.extents.y / gameObject.transform.localScale.y,
        -gameObject.GetComponent<Collider>().bounds.extents.z / gameObject.transform.localScale.z);


        
       
        MeshFilter filter = gameObject.GetComponent<MeshFilter>();
        var mesh = new Mesh();
        var verticies = new List<Vector3>();

        var indicies = new List<int>();
        for (int i = 0; i < GridSizeX+1; i++)
         {
            verticies.Add(new Vector3(i* ScaleCellCoeffX + FieldOffset.x, 0, FieldOffset.z));
            verticies.Add(new Vector3(i * ScaleCellCoeffX + FieldOffset.x, 0, GridSizeZ * ScaleCellCoeffZ + FieldOffset.z));

         


            indicies.Add(2 * i + 0);
            indicies.Add(2 * i + 1);

           
        }

        for (int j = 0; j < GridSizeZ+1; j++)
        {
           
            verticies.Add(new Vector3(FieldOffset.x, 0, j * ScaleCellCoeffZ + FieldOffset.z));
            verticies.Add(new Vector3(GridSizeX * ScaleCellCoeffX + FieldOffset.x, 0, j * ScaleCellCoeffZ + FieldOffset.z));

        

            indicies.Add(2 * j + 0 + 2*(GridSizeX+1));
            indicies.Add(2 * j + 1 + 2*(GridSizeX+1));
        }


        mesh.vertices = verticies.ToArray();
        GridVerticiesArray = verticies.ToArray();
        GridVerticiesArrayFinal = ExtraVerticiesArrayFromLineToGrid(GridVerticiesArray);


        mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
        filter.mesh = mesh;

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = Color.green;


        Vector3 [,] ExtraVerticiesArrayFromLineToGrid(Vector3[] InputArray)
        {
            Vector3 [,] OutputArray = new Vector3[GridSizeX+1, GridSizeZ+1];
            for (int i=0; i< GridSizeX;i++)
            {
                OutputArray[i, 0] = InputArray[i * 2];
                
                for (int j=1; j< GridSizeZ; j++)
                {   
                    OutputArray[i, j] = new Vector3(OutputArray[i, j - 1].x, OutputArray[i, j - 1].y, OutputArray[i, j - 1].z + ScaleCellCoeffZ);
                 
                 }
                

            }     


            return OutputArray;
        }
        
    }
}