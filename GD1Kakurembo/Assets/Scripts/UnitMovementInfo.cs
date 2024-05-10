using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementInfo : MonoBehaviour
{
    private UnitSelector unitSelector;
    public GameObject currentTile;
    public GameObject aimingTile;
    public string tileTag;

    private void Awake()
    {
        unitSelector = GetComponent<UnitSelector>();
    }

    
    private void Update()
    {
        if (unitSelector.clickedObject != null)
        {
            currentTile = unitSelector.clickedObject.GetComponent<CharacterProperties>().currentTile;
        }
        TileHoverAndClick();
    }

    private void TileHoverAndClick()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        { //IF we have hit something
            if (hit.transform.gameObject != currentTile && hit.transform.gameObject.CompareTag(tileTag)) 
            {
                aimingTile = hit.transform.gameObject;
            //Can do preemptive messages/options here before player clicks another tile
            }

            if (Input.GetMouseButtonDown(0) && currentTile != hit.transform.gameObject && unitSelector.clickedObject != null && hit.transform.gameObject.CompareTag(tileTag)) // also condition for leftover motivation
            {
                unitSelector.clickedObject.transform.position = hit.transform.position;
                unitSelector.clickedObject.GetComponent<CharacterProperties>().currentTile = hit.transform.gameObject;
            }
        }
    }

}
