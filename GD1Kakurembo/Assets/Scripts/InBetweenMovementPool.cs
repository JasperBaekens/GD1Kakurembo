using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InBetweenMovementPool : MonoBehaviour
{
    public MovementPointManager movementPointManager;
    public GameObject theUnit;

    public CharacterProperties characterProperties;

    private UnitMovementInfo currentUnitMovementInfo;

    public CharacterProperties.UnitMovementPool unitMovementpoolType;
    public CharacterProperties.Player currentUnitPlayer;

    public bool tileIsclicked = false;

    private void Update()
    {
        if (movementPointManager.myTurn1 && movementPointManager.playerOverall1.GetComponentInChildren<UnitSelector>().clickedObject !=null)
            theUnit = movementPointManager.playerOverall1.GetComponentInChildren<UnitSelector>().clickedObject;
        else if (movementPointManager.myTurn2 && movementPointManager.playerOverall2.GetComponentInChildren<UnitSelector>().clickedObject != null)
            theUnit = movementPointManager.playerOverall2.GetComponentInChildren<UnitSelector>().clickedObject;
        else 
            theUnit = null;

        if (theUnit != null)
        {
            currentUnitMovementInfo = theUnit.GetComponent<UnitMovementInfo>();
            characterProperties = theUnit.GetComponent<CharacterProperties>();
            currentUnitPlayer = characterProperties.unitPlayer;
            unitMovementpoolType = characterProperties.unitMovementpoolType;

            tileIsclicked = currentUnitMovementInfo.tileIsClicked;

            switch (unitMovementpoolType)
            {
                case CharacterProperties.UnitMovementPool.Commander:
                    if (currentUnitPlayer == CharacterProperties.Player.Player1)
                        if (!tileIsclicked)
                            currentUnitMovementInfo.currentMovementPool = movementPointManager.commanderMovementPool1;
                        else
                        {
                            movementPointManager.commanderMovementPool1 = currentUnitMovementInfo.currentMovementPool;
                            tileIsclicked = false;
                        }
                    else
                    {
                        if (!tileIsclicked)
                            currentUnitMovementInfo.currentMovementPool = movementPointManager.commanderMovementPool2;
                        else
                        {
                            movementPointManager.commanderMovementPool2 = currentUnitMovementInfo.currentMovementPool;
                            tileIsclicked = false;
                        }
                    }
                    break;
                case CharacterProperties.UnitMovementPool.Army:
                    if (currentUnitPlayer == CharacterProperties.Player.Player1)
                        if (!tileIsclicked)
                        {
                            currentUnitMovementInfo.currentMovementPool = movementPointManager.armyMovementPool1;
                        }
                        else
                        {
                            movementPointManager.armyMovementPool1 = currentUnitMovementInfo.currentMovementPool;
                            tileIsclicked = false;

                        }

                    else
                    {
                        if (!tileIsclicked)
                            currentUnitMovementInfo.currentMovementPool = movementPointManager.armyMovementPool2;
                        else
                        {
                            movementPointManager.armyMovementPool2 = currentUnitMovementInfo.currentMovementPool;
                            tileIsclicked = false;

                        }
                    }
                    break;
                case CharacterProperties.UnitMovementPool.Spy:
                    if (currentUnitPlayer == CharacterProperties.Player.Player1)
                        if (!tileIsclicked)
                            currentUnitMovementInfo.currentMovementPool = movementPointManager.spysMovementPool1;
                        else
                        {
                            movementPointManager.spysMovementPool1 = currentUnitMovementInfo.currentMovementPool;
                            tileIsclicked = false;
                        }
                    else
                    {
                        if (!tileIsclicked)
                            currentUnitMovementInfo.currentMovementPool = movementPointManager.spysMovementPool2;
                        else
                        {
                            movementPointManager.spysMovementPool2 = currentUnitMovementInfo.currentMovementPool;
                            tileIsclicked = false;
                        }
                    }
                    break;

            }
            currentUnitMovementInfo.tileIsClicked = tileIsclicked;
        }
    }






}
