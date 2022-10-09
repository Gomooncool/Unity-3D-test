
using UnityEngine;


class DragandDrop2 : MonoBehaviour
{
    private Color mouseOverColor = Color.yellow;
    private Color originalColor = Color.white;
    private bool dragging = false;
    public GameObject field;
    public RaycastHit hit;
    public Vector3 rayPoint;
    private Vector3[,] GridPointsArray;
    private Vector3 ClosestPoint;
    public Vector3 CardOriginalPos;
    public GameObject Arrow;
    private GameObject ArrowInst;
    public bool ArrowisCreated;
    public float InitDistance;
    

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = mouseOverColor;
    }




    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }





    void OnMouseDown()
    {
        transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        dragging = true;
        CardOriginalPos = transform.position;
        InitDistance = Arrow.GetComponent<MeshRenderer>().bounds.extents.z;

    }




    void OnMouseUp()
    {
       
        dragging = false;
        SnapToGrid();
        DestroyArrow();


        void DestroyArrow()
        {
            Destroy(ArrowInst, 0);
            ArrowisCreated = false;
        }



       




        void SnapToGrid()
        {  
            float ClosestPointDistance = 1000;
            ClosestPoint = new Vector3(100, 100, 100);

            GridPointsArray = GameObject.Find("Grid").GetComponent<GridMesh>().GridVerticiesArrayFinal;
            int XLengthArray = GameObject.Find("Grid").GetComponent<GridMesh>().GridSizeX;
            int ZLengthArray = GameObject.Find("Grid").GetComponent<GridMesh>().GridSizeZ;

            Vector3 CurrentCornerPos = new Vector3 (transform.position.x - gameObject.GetComponent<Collider>().bounds.extents.x, transform.position.y, transform.position.z - gameObject.GetComponent<Collider>().bounds.extents.z);
            
            for (int i=0; i<= XLengthArray; i++)
            {   
                for (int j = 0; j <= ZLengthArray; j++)
                    if (Vector3.Distance(GridPointsArray[i,j], CurrentCornerPos) < ClosestPointDistance)
                {
                    ClosestPoint = GridPointsArray[i,j];
                    ClosestPointDistance = Vector3.Distance(GridPointsArray[i,j], CurrentCornerPos);

                }
            }
            
            transform.position = new Vector3(ClosestPoint.x + gameObject.GetComponent<Collider>().bounds.extents.x, 0.01f, ClosestPoint.z + gameObject.GetComponent<Collider>().bounds.extents.z);

        }
    }








    void Update()
    {
        

        if (dragging)
        {
            CreateArrow();

            Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray1, out hit,Mathf.Infinity,9))
            {
                rayPoint = hit.point;              
                transform.position = new Vector3(rayPoint.x, transform.position.y, rayPoint.z);
            }

            ControlArrow();
          


        }

        void ControlArrow()
        {   
            ArrowInst.transform.rotation = Quaternion.LookRotation(transform.position - CardOriginalPos, Vector3.up);
            float ArrowScaleCoeff = Vector3.Distance(transform.position, CardOriginalPos) / Arrow.GetComponent<MeshRenderer>().bounds.extents.z;
            ArrowInst.transform.localScale = new Vector3(Arrow.transform.localScale.x * ArrowScaleCoeff / 3, Arrow.transform.localScale.y*ArrowScaleCoeff / 3f, Arrow.transform.localScale.z* ArrowScaleCoeff / 2f);

        }

        void CreateArrow()
        {
            if (Vector3.Distance(transform.position, CardOriginalPos) > InitDistance)
            
            {
                if (ArrowisCreated == false)
                {
                    ArrowInst = Instantiate(Arrow, CardOriginalPos + new Vector3(0, 0.3f, 0),
                    Quaternion.LookRotation(transform.position - CardOriginalPos, Vector3.up));
                    ArrowisCreated = true;
                }
            }


    

           
        }

    }

    
}