using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InBetweenMovementPool : MonoBehaviour
{
    public MovementPointManager movementPointManager;
    public GameObject theUnit;
    public float checkup;


    private UnitMovementInfo currentUnitMovementInfo;

    public UnitMovementPool unitMovementpoolType;
    public Player currentUnitPlayer;

    public enum UnitMovementPool
    {
        Commander,
        Army,
        Spy
    }
    public enum Player
    {
        Player1,
        Player2,
    }

    private void Awake()
    {
        currentUnitMovementInfo = theUnit.GetComponent<UnitMovementInfo>();
    }

    private void Update()
    {
        //currentUnitMovementInfo.currentMovementPool = currentUnitMovementPool;

        switch (unitMovementpoolType)
        {
            case UnitMovementPool.Commander:
                if (currentUnitPlayer == Player.Player1)
                    if (movementPointManager.currentFrameFirstInTurn1)
                        currentUnitMovementInfo.currentMovementPool = movementPointManager.commanderMovementPool1;
                    else
                        movementPointManager.commanderMovementPool1 = currentUnitMovementInfo.currentMovementPool;
                else
                {
                    if (movementPointManager.currentFrameFirstInTurn2)
                        currentUnitMovementInfo.currentMovementPool = movementPointManager.commanderMovementPool2;
                    else
                        movementPointManager.commanderMovementPool2 = currentUnitMovementInfo.currentMovementPool;
                }
                break;
            case UnitMovementPool.Army:
                if (currentUnitPlayer == Player.Player1)
                    if (movementPointManager.currentFrameFirstInTurn1)
                    {
                        currentUnitMovementInfo.currentMovementPool = movementPointManager.armyMovementPool1;
                    }
                    else
                    {
                        movementPointManager.armyMovementPool1 = currentUnitMovementInfo.currentMovementPool;
                        checkup++;
                    }

                else
                {
                    if (movementPointManager.currentFrameFirstInTurn2)
                        currentUnitMovementInfo.currentMovementPool = movementPointManager.armyMovementPool2;
                    else
                        movementPointManager.armyMovementPool2 = currentUnitMovementInfo.currentMovementPool;
                }
                break;
            case UnitMovementPool.Spy:
                if (currentUnitPlayer == Player.Player1)
                    if (movementPointManager.currentFrameFirstInTurn1)
                        currentUnitMovementInfo.currentMovementPool = movementPointManager.spysMovementPool1;
                    else
                        movementPointManager.spysMovementPool1 = currentUnitMovementInfo.currentMovementPool;
                else
                {
                    if (movementPointManager.currentFrameFirstInTurn2)
                        currentUnitMovementInfo.currentMovementPool = movementPointManager.spysMovementPool2;
                    else
                        movementPointManager.spysMovementPool2 = currentUnitMovementInfo.currentMovementPool;
                }
                break;
        }
        //currentUnitMovementPool = currentUnitMovementInfo.currentMovementPool;
    }







}
