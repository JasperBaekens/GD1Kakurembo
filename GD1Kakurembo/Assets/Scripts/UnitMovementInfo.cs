using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementInfo : MonoBehaviour
{
    //general Unitmovement
    public UnitSelector unitSelector;
    public GameObject currentTile;
    public GameObject aimingTile;
    public string tileTag;

    public bool tileIsClicked = false;


    //movement Rules
    public float currentMovementPool;
    public float movementCostCurrentAimingTile;

    public MovementPointManager movementPointManager;



    public TileProperties.TileType currentTileType;

    public bool IsNextTileAllowed = false;
    public Vector3 aimingtileLocation;
    //Faction Specific

    private void Awake()
    {
        movementPointManager = FindAnyObjectByType<MovementPointManager>();
    }


    private void Update()
    {
        if (unitSelector.clickedObject != null)
        {
            currentTile = unitSelector.clickedObject.GetComponent<CharacterProperties>().currentTile;
            currentTileType = currentTile.GetComponent<TileProperties>().tileType;
            TileHoverAndClick();
        }
    }

    private void TileHoverAndClick()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(r, out hit))
        { //IF we have hit something
            if (hit.transform.gameObject != currentTile && hit.transform.gameObject.CompareTag(tileTag)) //just aiming
            {

                aimingTile = hit.transform.gameObject;

                if (CheckIfTileAdjacent(hit))
                {
                    IsNextTileAllowed = true;
                    if (CheckIfDifferentTileType(aimingTile, currentTile)) 
                    {
                        movementCostCurrentAimingTile = 1;
                    }
                    else
                    {
                        switch (currentTileType) 
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
                    IsNextTileAllowed = false;
                    //communicate not allowed move
                }
                    //Can do preemptive messages/options here before player clicks another tile
            }

            if (Input.GetMouseButtonDown(0) && currentTile != hit.transform.gameObject && unitSelector.clickedObject != null && hit.transform.gameObject.CompareTag(tileTag)) // also condition for leftover motivation
            {
                if (CheckIfTileAdjacent(hit) && CheckIfEnoughMovementCostLeft())
                {
                    MoveToNewTile(hit);
                }




            }
        }
    }

    private bool CheckIfEnoughMovementCostLeft()
    {
        return (movementCostCurrentAimingTile <= currentMovementPool);
    }

    private bool CheckIfDifferentTileType(GameObject aim, GameObject current)
    {
        return (aim.GetComponent<TileProperties>().tileType != current.GetComponent<TileProperties>().tileType);
    }

    private bool CheckIfTileAdjacent(RaycastHit hit)
    {
        return (hit.transform.position == currentTile.transform.position + Vector3.right || hit.transform.position == currentTile.transform.position + Vector3.left || hit.transform.position == currentTile.transform.position + Vector3.forward || hit.transform.position == currentTile.transform.position + Vector3.back);
    }

    private void MoveToNewTile(RaycastHit hit)
    {
        currentMovementPool -= movementCostCurrentAimingTile;
        CaclulateAndGiveMotivation(hit);
        PingThings(hit);
        unitSelector.clickedObject.transform.position = hit.transform.position;
        unitSelector.clickedObject.GetComponent<CharacterProperties>().currentTile = hit.transform.gameObject;
        tileIsClicked = true;
    }

    private void PingThings(RaycastHit hit)
    {
        if (CheckIfDifferentTileType(hit.transform.gameObject, currentTile))
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
        if (CheckIfDifferentTileType(hit.transform.gameObject, currentTile))
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
