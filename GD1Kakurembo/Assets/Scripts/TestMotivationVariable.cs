using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMotivationVariable : MonoBehaviour
{
    public float motivationMax = 100;

    public float currentMotivationAmount = 100; 
    public Image motivationBar; //needed to change the fill based on motivation percentage


    // Update is called once per frame
    void Update()
    {
        //updates motivationbar visual on ui
        motivationBar.fillAmount = currentMotivationAmount / motivationMax;
    }
}
