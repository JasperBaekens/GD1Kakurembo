using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using static CharacterProperties;

public class PointingAtStuff : MonoBehaviour
{
    public GameObject ArrowPointing;
    private MovementPointManager movementPointManager;

    //what is done

    private bool _p1ShownCommander;
    private bool _p2ShownCommander;

    private bool p1ShownArmy;
    private bool p2ShownArmy;

    private bool p1ShownSpy;
    private bool p2ShownSpy;

    private bool p1ShownMotivation;
    private bool p2ShownMotivation;

    private bool p1ShownMovement;
    private bool p2ShownMovement;


    private bool p1ShownPing;
    private bool p2ShownPing;



    public Image EndTurn;


    public Image CommanderExplainer;
    public Image CommanderExplainer2;


    public Image Motivation;
    public Image MotivationFind;
    public Image MotivationLoseTurn;

    public Image Movement;
    public Image MovementAmount;
    private GameObject _startTile;

    public Image ArmyExplainer;
    public Image SpyExplainer;

    public Image PingExplainer;
    public Image PingExplainer2;

    private GameObject arrow1;
    private GameObject arrow2;





    private void Awake()
    {
        EndTurn.enabled = false;

        movementPointManager = FindObjectOfType<MovementPointManager>();
        CommanderExplainer.enabled = false;
        CommanderExplainer2.enabled = false;

        Motivation.enabled = false;
        MotivationFind.enabled = false;
        MotivationLoseTurn.enabled = false;

        Movement.enabled = false;
        MovementAmount.enabled = false;

        ArmyExplainer.enabled = false;
        SpyExplainer.enabled = false;

        PingExplainer.enabled = false;
        PingExplainer2.enabled = false;

    }

    private void Update()
    {
        if (movementPointManager.myTurn1 && AnyThingLeftToExplainP1())
        {
            if (!_p1ShownCommander && movementPointManager.TurnCounter == 1)
            {
                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.CommanderPlayer1.transform.position;
                _p1ShownCommander = true;
                CommanderExplainer.enabled = true;
                CommanderExplainer2.enabled = true;
            }
            else if (_p1ShownCommander && !p1ShownMovement && movementPointManager.UnitselectorPlayer1.clickedObject != null && movementPointManager.UnitselectorPlayer1.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Commander && movementPointManager.TurnCounter == 1)
            {
                CommanderExplainer.enabled = false;
                CommanderExplainer2.enabled = false;
                Destroy(arrow1);

                Movement.enabled = true;
                MovementAmount.enabled = true;
                p1ShownMovement = true;
                _startTile = movementPointManager.UnitselectorPlayer1.clickedObject.GetComponent<CharacterProperties>().currentTile;

                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.CommanderPlayer1.transform.position + Vector3.back;//other for p2

                arrow2 = Instantiate(ArrowPointing);
                arrow2.transform.position = movementPointManager.CommanderPlayer1.transform.position + Vector3.right;//other for p2
            }
            else if (p1ShownMovement && !p1ShownMotivation && movementPointManager.UnitselectorPlayer1 && movementPointManager.UnitselectorPlayer1.clickedObject.GetComponent<CharacterProperties>().currentTile != _startTile && movementPointManager.TurnCounter == 1)
            {
                Movement.enabled = false;
                MovementAmount.enabled = false;
                Destroy(arrow1);
                Destroy(arrow2);

                Motivation.enabled = true;
                MotivationFind.enabled = true;
                MotivationLoseTurn.enabled = true;

                p1ShownMotivation = true;

                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = Vector3.zero;

                arrow2 = Instantiate(ArrowPointing);
                arrow2.transform.position = Vector3.forward;
            }
            else if (p1ShownMotivation && Motivation.enabled && movementPointManager.TurnCounter == 1 && Input.GetMouseButtonDown(0))
            {
                Motivation.enabled = false;
                MotivationFind.enabled = false;
                MotivationLoseTurn.enabled = false;
                Destroy(arrow1);
                Destroy(arrow2);

                EndTurn.enabled = true;
            }
            //turn2
            else if (movementPointManager.TurnCounter == 2 && !p1ShownArmy)
            {
                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.ArmyPlayer1.transform.position;
                ArmyExplainer.enabled = true;
                p1ShownArmy = true;
                EndTurn.enabled = false;

            }
            else if (movementPointManager.TurnCounter == 2 && p1ShownArmy && movementPointManager.UnitselectorPlayer1.clickedObject && movementPointManager.UnitselectorPlayer1.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Army)
            {
                Destroy(arrow1);
                ArmyExplainer.enabled = false;
                EndTurn.enabled = true;

            }
            //turn3
            else if (movementPointManager.TurnCounter == 3 && !p1ShownSpy)
            {
                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.Spy1Player1.transform.position;
                arrow2 = Instantiate(ArrowPointing);
                arrow2.transform.position = movementPointManager.Spy2Player1.transform.position;
                SpyExplainer.enabled = true;
                p1ShownSpy = true;
                EndTurn.enabled = false;
            }
            else if (movementPointManager.TurnCounter == 3 && p1ShownSpy && movementPointManager.UnitselectorPlayer1.clickedObject && movementPointManager.UnitselectorPlayer1.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Spy)
            {
                Destroy(arrow1);
                Destroy(arrow2);
                SpyExplainer.enabled = false;
                EndTurn.enabled = true;

            }


            else if (!p1ShownPing && movementPointManager.PingsPlayer1.Count > 0 && CommanderExplainer.enabled == false && CommanderExplainer2.enabled == false && Motivation.enabled == false && MotivationFind.enabled == false && MotivationLoseTurn.enabled == false && Movement.enabled == false && MovementAmount.enabled == false && ArmyExplainer.enabled == false && SpyExplainer.enabled == false)
            {
                p1ShownPing = true;
                PingExplainer.enabled = true;
                PingExplainer2.enabled = true;
                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.PingsPlayer1[0].transform.position + Vector3.up;
                EndTurn.enabled = false;

            }
            else if (p1ShownPing && PingExplainer.enabled && Input.GetMouseButtonDown(0))
            {
                PingExplainer.enabled = false;
                PingExplainer2.enabled = false;
                Destroy(arrow1);
                EndTurn.enabled = true;
            }
        }





        if (movementPointManager.myTurn2 && AnyThingLeftToExplainP2())
        {
            if (!_p2ShownCommander && movementPointManager.TurnCounter == 1)
            {
                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.CommanderPlayer2.transform.position;
                _p2ShownCommander = true;
                CommanderExplainer.enabled = true;
                CommanderExplainer2.enabled = true;
            }
            else if (_p2ShownCommander && !p2ShownMovement && movementPointManager.UnitselectorPlayer2.clickedObject != null && movementPointManager.UnitselectorPlayer2.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Commander && movementPointManager.TurnCounter == 1)
            {
                CommanderExplainer.enabled = false;
                CommanderExplainer2.enabled = false;
                Destroy(arrow1);

                Movement.enabled = true;
                MovementAmount.enabled = true;
                p2ShownMovement = true;
                _startTile = movementPointManager.UnitselectorPlayer2.clickedObject.GetComponent<CharacterProperties>().currentTile;

                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.CommanderPlayer2.transform.position + Vector3.forward;

                arrow2 = Instantiate(ArrowPointing);
                arrow2.transform.position = movementPointManager.CommanderPlayer2.transform.position + Vector3.left;
            }
            else if (p2ShownMovement && !p2ShownMotivation && movementPointManager.UnitselectorPlayer2 && movementPointManager.UnitselectorPlayer2.clickedObject.GetComponent<CharacterProperties>().currentTile != _startTile && movementPointManager.TurnCounter == 1)
            {
                Movement.enabled = false;
                MovementAmount.enabled = false;
                Destroy(arrow1);
                Destroy(arrow2);

                Motivation.enabled = true;
                MotivationFind.enabled = true;
                MotivationLoseTurn.enabled = true;

                p2ShownMotivation = true;

                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = Vector3.zero;

                arrow2 = Instantiate(ArrowPointing);
                arrow2.transform.position = Vector3.forward;
            }
            else if (p2ShownMotivation && Motivation.enabled && movementPointManager.TurnCounter == 1 && Input.GetMouseButtonDown(0))
            {
                Motivation.enabled = false;
                MotivationFind.enabled = false;
                MotivationLoseTurn.enabled = false;
                Destroy(arrow1);
                Destroy(arrow2);

                EndTurn.enabled = true;
            }
            //turn2
            else if (movementPointManager.TurnCounter == 2 && !p2ShownArmy)
            {
                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.ArmyPlayer2.transform.position;
                ArmyExplainer.enabled = true;
                p2ShownArmy = true; 
                EndTurn.enabled = false;
            }
            else if (movementPointManager.TurnCounter == 2 && p2ShownArmy && movementPointManager.UnitselectorPlayer2.clickedObject && movementPointManager.UnitselectorPlayer2.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Army)
            {
                Destroy(arrow1);
                ArmyExplainer.enabled = false;
                EndTurn.enabled = true;

            }
            //turn3
            else if (movementPointManager.TurnCounter == 3 && !p2ShownSpy)
            {
                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.Spy1Player2.transform.position;
                arrow2 = Instantiate(ArrowPointing);
                arrow2.transform.position = movementPointManager.Spy2Player2.transform.position;
                SpyExplainer.enabled = true;
                p2ShownSpy = true;
                EndTurn.enabled = false;
            }
            else if (movementPointManager.TurnCounter == 3 && p2ShownSpy && movementPointManager.UnitselectorPlayer2.clickedObject && movementPointManager.UnitselectorPlayer2.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Spy)
            {
                Destroy(arrow1);
                Destroy(arrow2);
                SpyExplainer.enabled = false;
                EndTurn.enabled = true;

            }




            else if (!p2ShownPing && movementPointManager.PingsPlayer2.Count > 0 && CommanderExplainer.enabled == false && CommanderExplainer2.enabled == false && Motivation.enabled == false && MotivationFind.enabled == false && MotivationLoseTurn.enabled == false && Movement.enabled == false && MovementAmount.enabled == false && ArmyExplainer.enabled == false && SpyExplainer.enabled == false)
            {
                p2ShownPing = true;
                PingExplainer.enabled = true;
                PingExplainer2.enabled = true;
                arrow1 = Instantiate(ArrowPointing);
                arrow1.transform.position = movementPointManager.PingsPlayer2[0].transform.position + Vector3.up;
                EndTurn.enabled = false;

            }
            else if (p2ShownPing && PingExplainer.enabled && Input.GetMouseButtonDown(0))
            {
                PingExplainer.enabled = false;
                PingExplainer2.enabled = false;
                Destroy(arrow1);
                EndTurn.enabled = true;

            }
        }

    }

    private bool AnyThingLeftToExplainP1()
    {
        return !(_p1ShownCommander && p1ShownArmy && p1ShownSpy && p1ShownMotivation && p1ShownMovement && p1ShownPing && !ArmyExplainer.enabled && !SpyExplainer.enabled && !PingExplainer.enabled);   
    }
    private bool AnyThingLeftToExplainP2()
    {
        return !(_p2ShownCommander && p2ShownArmy && p2ShownSpy && p2ShownMotivation && p2ShownMovement && p2ShownPing && !ArmyExplainer.enabled && !SpyExplainer.enabled && !PingExplainer.enabled);
    }

}
