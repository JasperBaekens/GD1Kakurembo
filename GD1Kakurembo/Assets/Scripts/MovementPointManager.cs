using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPointManager : MonoBehaviour
{
    //player1
    public bool myTurn1;
    public bool previousFrameMyTurn1 = false;
    public bool currentFrameFirstInTurn1;

    public GameObject playerOverall1;

    public GameObject CommanderPlayer1;
    public GameObject ArmyPlayer1;
    public GameObject Spy1Player1;
    public GameObject Spy2Player1;
    public GameObject Spy3Player1;


    public int commanderDiceResult1;
    public int armyDiceResult1;
    public int spysDiceResult1;

    public float commanderMovementPool1;
    public float armyMovementPool1;
    public float spysMovementPool1;

    public float motivationMaxPlayer1 = 100 ;
    public float motivationCurrentPlayer1 = 100;

    //player2
    public bool myTurn2;
    public bool previousFrameMyTurn2 = false;
    public bool currentFrameFirstInTurn2;

    public GameObject playerOverall2;

    public GameObject CommanderPlayer2;
    public GameObject ArmyPlayer2;
    public GameObject Spy1Player2;
    public GameObject Spy2Player2;
    public GameObject Spy3Player2;


    public int commanderDiceResult2;
    public int armyDiceResult2;
    public int spysDiceResult2;

    public float commanderMovementPool2;
    public float armyMovementPool2;
    public float spysMovementPool2;

    public float motivationMaxPlayer2 = 100;
    public float motivationCurrentPlayer2 = 100;


    //overal
    public int TurnCounter;

    public bool Player1Won = false;
    public bool Player2Won = false;

    public float MotivationDecreasePerTurn = 5;

    private void Update()
    {
        UpdateTurn();

        UpdateMotivation();

        GenerateNewDiceRolls1();
        GenerateNewDiceRolls2();

        AddMovementPlayer1();
        AddMovementPlayer2();

        CheckWinPlayer1();
        CheckWinPlayer2();


    }

    private void UpdateMotivation()
    {
        if (currentFrameFirstInTurn1)
            motivationCurrentPlayer1 -= MotivationDecreasePerTurn;

        if (currentFrameFirstInTurn2)
            motivationCurrentPlayer2 -= MotivationDecreasePerTurn;

        if (playerOverall1.activeSelf)
        {
            playerOverall1.GetComponentInChildren<TestMotivationVariable>().motivationMax = motivationMaxPlayer1;
            motivationCurrentPlayer1 = Math.Min(motivationMaxPlayer1, motivationCurrentPlayer1);
            playerOverall1.GetComponentInChildren<TestMotivationVariable>().currentMotivationAmount = motivationCurrentPlayer1;
        }
        if (playerOverall2.activeSelf)
        {
            playerOverall2.GetComponentInChildren<TestMotivationVariable>().motivationMax = motivationMaxPlayer2;
            motivationCurrentPlayer2 = Math.Min(motivationMaxPlayer2, motivationCurrentPlayer2);
            playerOverall2.GetComponentInChildren<TestMotivationVariable>().currentMotivationAmount = motivationCurrentPlayer2;
        }
    }
    private void CheckWinPlayer1()
    {
        if (ArmyPlayer1.GetComponent<CharacterProperties>().currentTile == CommanderPlayer2.GetComponent<CharacterProperties>().currentTile || motivationCurrentPlayer2 <=0 )
            Player1Won = true;
    }

    private void CheckWinPlayer2()
    {
        if (ArmyPlayer2.GetComponent<CharacterProperties>().currentTile == CommanderPlayer1.GetComponent<CharacterProperties>().currentTile || motivationCurrentPlayer1 <= 0)
            Player2Won = true;
    }


    private void GenerateNewDiceRolls1()
    {
        if (currentFrameFirstInTurn1) //make this talk with diceresults!!!!!
        {
            commanderDiceResult1 = UnityEngine.Random.Range(1, 6);
            spysDiceResult1 = UnityEngine.Random.Range(1, 6);
            armyDiceResult1 = UnityEngine.Random.Range(1, 6);
        }
    }

    private void GenerateNewDiceRolls2()
    {
        if (currentFrameFirstInTurn2) //make this talk with diceresults!!!!!
        {
            commanderDiceResult2 = UnityEngine.Random.Range(1, 6);
            spysDiceResult2 = UnityEngine.Random.Range(1, 6);
            armyDiceResult2 = UnityEngine.Random.Range(1, 6);
        }
    }


    private void AddMovementPlayer1()
    {
        if (currentFrameFirstInTurn1) //assign movement here for player1 in beginning of turn
        {
            spysMovementPool1 += armyMovementPool1 + commanderMovementPool1 + spysDiceResult1;
            commanderMovementPool1 = commanderDiceResult1;
            armyMovementPool1 = armyDiceResult1;

        }
    }
    private void AddMovementPlayer2()
    {
        if (currentFrameFirstInTurn2) //assign movement here for player 2 in beginning of turn
        {
            spysMovementPool2 += armyMovementPool2 + commanderMovementPool2 + spysDiceResult2;
            commanderMovementPool2 = commanderDiceResult2;
            armyMovementPool2 = armyDiceResult2;
        }
    }

    private void UpdateTurn()
    {
        myTurn1 = false;
        currentFrameFirstInTurn1 = false;
        myTurn2 = false;
        currentFrameFirstInTurn2 = false;



        if (playerOverall1.activeSelf)
        {
            myTurn1 = true;

            if (!previousFrameMyTurn1)
            { 
                currentFrameFirstInTurn1 = true;
                TurnCounter++;
            }

            previousFrameMyTurn1 = true;
            previousFrameMyTurn2 = false;
        }
        else if (playerOverall2.activeSelf)
        {
            myTurn2 = true;

            if (!previousFrameMyTurn2)
                currentFrameFirstInTurn2 = true;

            previousFrameMyTurn2 = true;
            previousFrameMyTurn1 = false;
        }
    }
}
