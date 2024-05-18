using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;


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
    
    //movemntfix
    private GameObject currentTileSelectedUnit;
    private TileProperties.TileType currentTileSelectedUnitType;
    private GameObject aimingTile;

    //private bool IsNextTileAllowed = false; //could do messages with this
    private float movementCostCurrentAimingTile;
    private MovementPool movementPoolCurrentSelectedUnit;
    private MovementPointManager movementPointManager;

    private void Awake()
    {
        movementPointManager = FindObjectOfType<MovementPointManager>();
    }


    // Update is called once per frame
    void Update()
    {
        HoverHighlight();
        SelectHighLight();

        MoveFunction();
    }

    private void MoveFunction()
    {
        if (clickedObject != null)
        {
            //Debug.Log($"clickedObject {clickedObject}");
            //Debug.Log($"Movementpool:{MovementPool.name} have:{MovementPool.MovementPoolCurrent}"); //all scripts running at same time might be the issue
            currentTileSelectedUnit = clickedObject.GetComponent<CharacterProperties>().currentTile;
            currentTileSelectedUnitType = currentTileSelectedUnit.GetComponent<TileProperties>().tileType;
            movementPoolCurrentSelectedUnit = clickedObject.GetComponent<CharacterProperties>().MovementPool;
            TileHoverAndClick();
        }

    }

    private void SelectHighLight() //was here pre movementfix attempt
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


    private void HoverHighlight() //was here pre movementfix attempt
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


    private void HoverHighlightCurrentObject()//washerepremovementfixattempt
    {
        if (currentObject != clickedObject)
        {
            oldColor = currentObject.GetComponent<MeshRenderer>().material.GetColor("_Color");
            Color newColor = new Color(oldColor.r + highlightFactor, oldColor.g + highlightFactor, oldColor.b + highlightFactor, oldColor.a);
            currentObject.GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
        }
    }

    
    private void HoverRestoreCurrentObject()//Restores the current object to it's former state. //washerepremovemntfix
    {
        if (currentObject != null && currentObject != clickedObject && !DeClickHappened)
        { //IF we actually have an object to restore
            currentObject.GetComponent<MeshRenderer>().material.SetColor("_Color", oldColor);
            currentObject = null;
        }
        DeClickHappened = false;
    }

    private void TileHoverAndClick()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        { //IF we have hit something
            if (hit.transform.gameObject != currentTileSelectedUnit && hit.transform.gameObject.CompareTag("Tile")) //just aiming
            {

                aimingTile = hit.transform.gameObject;

                if (CheckIfTileAdjacent(hit))
                {
                    //IsNextTileAllowed = true;
                    if (CheckIfDifferentTileType(aimingTile, currentTileSelectedUnit))
                    {
                        movementCostCurrentAimingTile = 1f;
                    }
                    else
                    {
                        switch (currentTileSelectedUnitType)
                        {
                            case TileProperties.TileType.RoadTile:
                                movementCostCurrentAimingTile = 0.5f;
                                break;
                            case TileProperties.TileType.WaterTile:
                                movementCostCurrentAimingTile = 1f;
                                break;
                            case TileProperties.TileType.BaseTile:
                                movementCostCurrentAimingTile = 2f;
                                break;
                            default:
                                movementCostCurrentAimingTile = 1f;
                                break;
                        }
                    }
                }
                else
                {
                    //IsNextTileAllowed = false;
                    //communicate not allowed move
                }
                //Can do preemptive messages/options here before player clicks another tile
            }

            if (Input.GetMouseButtonDown(0) && currentTileSelectedUnit != aimingTile && clickedObject != null && hit.transform.gameObject.CompareTag("Tile")) // also condition for leftover motivation
            {
                //Debug.Log($"Check If Tile Adjecent: {CheckIfTileAdjacent(hit)} ; Check If Enough Movement Cost Left(): {CheckIfEnoughMovementCostLeft()}");
                //Debug.Log($"CheckIfEnoughMovementCostLeft: needed:{movementCostCurrentAimingTile} Movementpool:{movementPoolCurrentSelectedUnit.name} have:{movementPoolCurrentSelectedUnit.MovementPoolCurrent}"); //wrong movementpool, switches even between clicks???
                if (CheckIfTileAdjacent(hit) && CheckIfEnoughMovementCostLeft())
                {
                    MoveToNewTile(hit);
                }

            }
        }
    }

    private bool CheckIfEnoughMovementCostLeft()
    {
        return (movementCostCurrentAimingTile <= movementPoolCurrentSelectedUnit.MovementPoolCurrent);
    }

    private bool CheckIfDifferentTileType(GameObject aim, GameObject current)
    {
        return (aim.GetComponent<TileProperties>().tileType != current.GetComponent<TileProperties>().tileType);
    }

    private bool CheckIfTileAdjacent(RaycastHit hit)
    {
        return (hit.transform.position == currentTileSelectedUnit.transform.position + Vector3.right || hit.transform.position == currentTileSelectedUnit.transform.position + Vector3.left || hit.transform.position == currentTileSelectedUnit.transform.position + Vector3.forward || hit.transform.position == currentTileSelectedUnit.transform.position + Vector3.back);
    }

    private void MoveToNewTile(RaycastHit hit)
    {
        //if (!tileIsClicked)
        {
            movementPoolCurrentSelectedUnit.MovementPoolCurrent -= movementCostCurrentAimingTile; //could this be the problem???

            CaclulateAndGiveMotivation(hit);
            PingThings(hit);
            clickedObject.transform.position = hit.transform.position;
            clickedObject.GetComponent<CharacterProperties>().currentTile = hit.transform.gameObject;
            //tileIsClicked = true;
        }
    }

    private void PingThings(RaycastHit hit)
    {
        if (CheckIfDifferentTileType(hit.transform.gameObject, currentTileSelectedUnit))
        {
            if (hit.transform.gameObject.GetComponent<TileProperties>().tileType == TileProperties.TileType.ForestTile)
            {
                if (movementPointManager.myTurn1)
                {
                    movementPointManager.tilesToPingPlayer2.Add(hit.transform.gameObject);
                }
                if (movementPointManager.myTurn2)
                {
                    movementPointManager.tilesToPingPlayer1.Add(hit.transform.gameObject);

                }
            }
            if (hit.transform.gameObject.GetComponent<TileProperties>().tileType == TileProperties.TileType.VillageTile)
            {
                if (movementPointManager.myTurn1)
                {
                    movementPointManager.tilesToPingPlayer2.Add(hit.transform.gameObject);
                }
                if (movementPointManager.myTurn2)
                {
                    movementPointManager.tilesToPingPlayer1.Add(hit.transform.gameObject);
                }
            }
            if (hit.transform.gameObject.GetComponent<TileProperties>().tileType == TileProperties.TileType.RoadTile)
            {
                if (movementPointManager.myTurn1)
                {
                    movementPointManager.tilesToPingPlayer2.Add(hit.transform.gameObject);
                }
                if (movementPointManager.myTurn2)
                {
                    movementPointManager.tilesToPingPlayer1.Add(hit.transform.gameObject);
                }
            }


        }
    }

    private void CaclulateAndGiveMotivation(RaycastHit hit)
    {
        if (CheckIfDifferentTileType(hit.transform.gameObject, currentTileSelectedUnit))
        {
            if (hit.transform.gameObject.GetComponent<TileProperties>().tileType == TileProperties.TileType.ForestTile)
            {
                if (movementPointManager.myTurn1) //imperial get 5
                {
                    movementPointManager.motivationCurrentPlayer1 += 5;
                }
                if (movementPointManager.myTurn2) //rebels get 10
                {
                    movementPointManager.motivationCurrentPlayer2 += 10;

                }
            }
            if (hit.transform.gameObject.GetComponent<TileProperties>().tileType == TileProperties.TileType.VillageTile)
            {
                if (movementPointManager.myTurn1) //imperial get 10
                {
                    movementPointManager.motivationCurrentPlayer1 += 10;
                }
                if (movementPointManager.myTurn2) //rebels get 5
                {
                    movementPointManager.motivationCurrentPlayer2 += 5;

                }
            }

        }

    }
}



