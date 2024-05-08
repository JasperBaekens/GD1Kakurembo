using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitSelector : MonoBehaviour
{
    private GameObject currentObject; //The currently highlighted object
    private Color oldColor; //The old color of the currentObject
    private Material materialBeforeHighlight;



    private float highlightFactor = 0.3f; //How much should the object be highlighted
    public GameObject clickedObject; //the current selected object clicked on
    public Material materialHighlightClicked;
    public Material materialHighlightClickedBefore;


    // Update is called once per frame
    void Update()
    {
        HoverHighlight();
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



            if (Input.GetMouseButtonDown(0) && clickedObject != null && clickedObject == currentObject)
            {
                clickedObject.GetComponent<MeshRenderer>().material = materialHighlightClickedBefore;
                clickedObject = null;
            }
            else if (Input.GetMouseButtonDown(0) && currentObject != null && clickedObject != currentObject && clickedObject == null)
            {
                HoverRestoreCurrentObjectBeforeClick();
                clickedObject = currentObject;
                materialHighlightClickedBefore = clickedObject.GetComponent<MeshRenderer>().material;
                clickedObject.GetComponent<MeshRenderer>().material = materialHighlightClicked;
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
            Renderer r = currentObject.GetComponent(typeof(Renderer)) as Renderer;
            oldColor = r.material.GetColor("_Color");
            Color newColor = new Color(oldColor.r + highlightFactor, oldColor.g + highlightFactor, oldColor.b + highlightFactor, oldColor.a);
            r.material.SetColor("_Color", newColor);
        }
    }

    //Restores the current object to it's former state.
    private void HoverRestoreCurrentObject()
    {
        if (currentObject != null && currentObject != clickedObject)
        { //IF we actually have an object to restore
            Renderer r = currentObject.GetComponent(typeof(Renderer)) as Renderer;
            r.material.SetColor("_Color", oldColor);
            currentObject = null;
        }
    }
    private void HoverRestoreCurrentObjectBeforeClick()
    {
        Renderer r = currentObject.GetComponent(typeof(Renderer)) as Renderer;
        r.material.SetColor("_Color", oldColor);
    }

}

