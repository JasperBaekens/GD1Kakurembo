using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMotivationVariable : MonoBehaviour
{
    public const float motivationMax = 10;
    public const float motivationMin = 0;

    public float currentMotivationAmount; 
    public Image motivationBar; //needed to change the fill based on motivation percentage


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("o") && currentMotivationAmount < motivationMax) //if motivationbar not full increases current motivation by one
        {
            currentMotivationAmount++;
        }
        if (Input.GetKeyDown("p") && currentMotivationAmount > motivationMin) //if motivationbar not empty deccreases current motivation by one
        {
            currentMotivationAmount--;
        }

        //updates motivationbar visual on ui
        motivationBar.fillAmount = currentMotivationAmount / motivationMax;
    }
}
