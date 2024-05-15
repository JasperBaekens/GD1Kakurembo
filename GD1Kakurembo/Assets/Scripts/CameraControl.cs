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
    public Vector3 NewPosition;
    public Vector3 MousePosition;
    public float MouseMargin = 0.1f;

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        TranslateX = Input.GetAxis("Horizontal");

        TranslateZ = Input.GetAxis("Vertical");

        TranslateXZ = new Vector3(TranslateX, 0, TranslateZ).normalized * Speed * Time.deltaTime;

        NewPosition = transform.position + TranslateXZ; //new Vector3(TranslateX, 0, TranslateZ);



        if (TranslateXZ == Vector3.zero)
        {
            MoveWithScreen();

        }



        if (!((NewPosition.x > LimitX.x) || (NewPosition.x < (LimitX.x * -1)) || (NewPosition.z < (-LimitZ.z)) || (NewPosition.z > LimitZ.z)))
        {
            transform.Translate(TranslateXZ);
            //Debug.Log("We Good");

        }





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

        TranslateXZ = new Vector3(TranslateX, 0, TranslateZ).normalized * Speed * Time.deltaTime;

        NewPosition = transform.position + TranslateXZ; //new Vector3(TranslateX, 0, TranslateZ);

    }




}