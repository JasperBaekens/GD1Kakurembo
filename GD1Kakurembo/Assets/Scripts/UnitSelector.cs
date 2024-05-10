using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class UnitSelector : MonoBehaviour
{
    private GameObject currentObject; //The currently highlighted object
    private Color oldColor; //The old color of the currentObject

    private bool DeClickHappened = false;

    private float highlightFactor = 0.3f; //How much should the object be highlighted
    public GameObject clickedObject; //the current selected object clicked on
    public Material materialHighlightClicked;
    public Material materialHighlightClickedBefore;
    public string characterTag;

    // Update is called once per frame
    void Update()
    {
        HoverHighlight();
        SelectHighLight();
    }

    private void SelectHighLight()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        { //IF we have hit something
            if (Input.GetMouseButtonDown(0) && clickedObject != null && clickedObject == hit.transform.gameObject)
            {
                clickedObject.GetComponent<MeshRenderer>().material = materialHighlightClickedBefore;
                clickedObject = null;
                DeClickHappened = true;
            }

            else if (Input.GetMouseButtonDown(0) && clickedObject != hit.transform.gameObject && clickedObject == null && hit.transform.gameObject.CompareTag(characterTag))
            {
                HoverRestoreCurrentObject();
                clickedObject = hit.transform.gameObject;
                materialHighlightClickedBefore = clickedObject.GetComponent<MeshRenderer>().material;
                clickedObject.GetComponent<MeshRenderer>().material = materialHighlightClicked;
                DeClickHappened = false;
            }
        }
    }


    private void HoverHighlight()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        { //IF we have hit something
            if (currentObject == null)
            { //IF we haved no current object
                currentObject = hit.transform.gameObject; //save the object
                HoverHighlightCurrentObject(); //and highlight it
            }
            else if (hit.transform != currentObject.transform)
            { //ELSE IF we have hit a different object
                HoverRestoreCurrentObject(); //THEN restore the old object
                currentObject = hit.transform.gameObject; //save the new object
                HoverHighlightCurrentObject(); //and highlight it
            }
        }
        else //ELSE no object was hit
        {
            HoverRestoreCurrentObject(); //THEN restore the old object
        }
    }


    private void HoverHighlightCurrentObject()
    {
        if (currentObject != clickedObject)
        {
            oldColor = currentObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            Color newColor = new Color(oldColor.r + highlightFactor, oldColor.g + highlightFactor, oldColor.b + highlightFactor, oldColor.a);
            currentObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }
    }

    //Restores the current object to it's former state.
    private void HoverRestoreCurrentObject()
    {
        if (currentObject != null && currentObject != clickedObject && !DeClickHappened)
        { //IF we actually have an object to restore
            currentObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
            currentObject = null;
        }
        DeClickHappened = false;
    }
}

