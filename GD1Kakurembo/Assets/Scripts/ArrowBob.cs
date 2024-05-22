using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBob : MonoBehaviour
{
    public float BobSpeed = 6f;
    public float BobHeightDynamic = 0.002f;


    private void Update()
    {
        transform.localPosition += new Vector3(0, Mathf.Sin(Time.time * BobSpeed) * BobHeightDynamic, 0);
    }





}
