using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryControl : MonoBehaviour
{
    public float Speed = 5f;

    public Vector3 LimitX = new Vector3(50, 0, 0);
    public Vector3 LimitZ = new Vector3(0, 0, 50);
    public float TranslateX;
    public float TranslateZ;
    public Vector3 TranslateXZ;
    public Vector3 NewPositionLimitTest;
    public Vector3 NewPositionMax;
    public Vector3 NewPositionMaxMin;
    public Vector3 NewPosition;
    public Vector3 MousePosition;
    public float MouseMargin = 0.1f;
    public float cameraHeight = 3f;

    // Update is called once per frame
    void Update()
    {
        TranslateX = Input.GetAxis("Horizontal");

        TranslateZ = Input.GetAxis("Vertical");

        TranslateXZ = new Vector3(TranslateZ, 0, -TranslateX).normalized * Speed * Time.deltaTime;


        if (TranslateXZ == Vector3.zero)
        {
            MoveWithScreen();
        }

        NewPositionLimitTest = transform.position + TranslateXZ; //new Vector3(TranslateX, 0, TranslateZ);
        NewPositionMax = new Vector3(Math.Min(NewPositionLimitTest.x, LimitX.x), cameraHeight, Math.Min(NewPositionLimitTest.z, LimitZ.z));
        NewPositionMaxMin = new Vector3(Math.Max(NewPositionMax.x, -LimitX.x), cameraHeight, Math.Max(NewPositionMax.z, -LimitZ.z));

        transform.position = NewPositionMaxMin;
        //if ((NewPosition.x >= LimitX.x))
        //{
        //    //Debug.Log("out of X+");
        //    TranslateXZ = new Vector3(TranslateX, 0, Math.Min(0, TranslateZ)).normalized * Speed * Time.deltaTime;
        //    NewPosition = transform.position + TranslateXZ; //new Vector3(TranslateX, 0, TranslateZ);

        //}
        //if (NewPosition.x <= (-LimitX.x))
        //{
        //    //Debug.Log("out of X-");
        //    TranslateXZ = new Vector3(TranslateX, 0, Math.Max(0, TranslateZ)).normalized * Speed * Time.deltaTime;
        //    NewPosition = transform.position + TranslateXZ; //new Vector3(TranslateX, 0, TranslateZ);

        //}
        //if ((NewPosition.z >= LimitZ.z))
        //{
        //    //Debug.Log("out of Z+");
        //    TranslateXZ = new Vector3(Math.Max(0, TranslateX), 0, TranslateZ).normalized * Speed * Time.deltaTime;
        //    NewPosition = transform.position + TranslateXZ; //new Vector3(TranslateX, 0, TranslateZ);

        //}
        //if (NewPosition.z <= (-LimitZ.z))
        //{
        //    //Debug.Log("out of Z-");
        //    TranslateXZ = new Vector3(Math.Min(0, TranslateX), 0, TranslateZ).normalized * Speed * Time.deltaTime;
        //    NewPosition = transform.position + TranslateXZ; //new Vector3(TranslateX, 0, TranslateZ);

        //}

        //Debug.Log("We Good");





    }

    private void MoveWithScreen()
    {
        MousePosition = Input.mousePosition;
        MousePosition = Camera.main.ScreenToViewportPoint(MousePosition);

        TranslateX = 0;
        TranslateZ = 0;

        //Debug.Log(MousePosition);

        if (MousePosition.x > 1 - MouseMargin)
        {
            TranslateX = 1;
        }

        if (MousePosition.y > 1 - MouseMargin)
        {
            TranslateZ = 1;
        }

        if (MousePosition.x < MouseMargin)
        {
            TranslateX = -1;
        }

        if (MousePosition.y < MouseMargin)
        {
            TranslateZ = -1;
        }

        TranslateXZ = new Vector3(TranslateZ, 0, - TranslateX).normalized * Speed * Time.deltaTime;
    }




}
