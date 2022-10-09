using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameramove : MonoBehaviour
 
{
    private float x;
    private float y;
    private Vector3 rotateValue;
    public Transform objoff;
    private Vector3 offset;
    public void Start()
    {
       offset = transform.position - objoff.position;
    }
    void Update()
    {
        
        transform.position = objoff.position + offset;
        y = Input.GetAxis("Mouse X");
        x = Input.GetAxis("Mouse Y");
        Debug.Log(x);
        Debug.Log(y);
        rotateValue = new Vector3(x*2, y * -2, 0);
        transform.eulerAngles = transform.eulerAngles - rotateValue;
    }
}
