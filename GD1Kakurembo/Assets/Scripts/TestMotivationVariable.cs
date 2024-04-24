using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMotivationVariable : MonoBehaviour
{

    [Range(0,10)] public int motivationAmount = 2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") && motivationAmount < 10)
        {
            motivationAmount++;
        }
        if (Input.GetKeyDown("s") && motivationAmount > 0)
        {
            motivationAmount--;
        }

    }
}
