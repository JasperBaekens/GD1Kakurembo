using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
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

    public float motivationMaxPlayer1 = 100 ;
    public float motivationCurrentPlayer1 = 100;

    public List<GameObject> tilesToPingPlayer1;
    public List<GameObject> PingsPlayer1;
    public List<GameObject> tilesToPingPlayer1LastFrame;
    public List<GameObject> tilesToPingPlayer1SpyCommand;


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


    public float motivationMaxPlayer2 = 100;
    public float motivationCurrentPlayer2 = 100;

    public List<GameObject> tilesToPingPlayer2;
    public List<GameObject> PingsPlayer2;
    public List<GameObject> tilesToPingPlayer2LastFrame;
    public List<GameObject> tilesToPingPlayer2SpyCommand;



    //overal
    public GameObject PrefabPing; 
    public GameObject PrefabPingSpyComm;
    public int TurnCounter;

    public bool Player1Won = false;
    public bool Player2Won = false;

    public float MotivationDecreasePerTurn = 5;
    public bool Commander1Found = false;
    public bool Commander2Found = false;



    //movement tryfix
    public MovementPool MovementPoolSpyPlayer1;
    public MovementPool MovementPoolSpyPlayer2;
    public MovementPool MovementPoolArmyPlayer1;
    public MovementPool MovementPoolArmyPlayer2;
    public MovementPool MovementPoolCommanderPlayer1;
    public MovementPool MovementPoolCommanderPlayer2;


    //Deselect all units on first frame in turn
    public UnitSelector UnitselectorPlayer1;
    public UnitSelector UnitselectorPlayer2;

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

        ClearPingListPlayer1();
        ClearPingListPlayer2();

        CheckForScoutCommanderInteraction();

        DrawPingListPlayer1();
        DrawPingListPlayer2();

        DeslectCurrentUnits();
    }

    private void DeslectCurrentUnits()
    {
        if (currentFrameFirstInTurn1 || currentFrameFirstInTurn2)
        {
            if (UnitselectorPlayer1.clickedObject != null) 
            {
                UnitselectorPlayer1.DeselectCurrentSelectedUnit();
            }
            if (UnitselectorPlayer2.clickedObject != null)
            {
                UnitselectorPlayer2.DeselectCurrentSelectedUnit();
            }
        }
    }

    private void CheckForScoutCommanderInteraction()
    {
        if (currentFrameFirstInTurn1 || currentFrameFirstInTurn2)
        {
            Commander1Found = false;
            Commander2Found = false;

        }



        if (!Commander1Found & (
            CommanderPlayer1.GetComponent<CharacterProperties>().currentTile == Spy1Player2.GetComponent<CharacterProperties>().currentTile ||
            CommanderPlayer1.GetComponent<CharacterProperties>().currentTile == Spy2Player2.GetComponent<CharacterProperties>().currentTile ||
            CommanderPlayer1.GetComponent<CharacterProperties>().currentTile == Spy3Player2.GetComponent<CharacterProperties>().currentTile
            ))
        {
            Commander1Found = true;
            tilesToPingPlayer2.Add(CommanderPlayer1.GetComponent<CharacterProperties>().currentTile);
            tilesToPingPlayer1.Add(CommanderPlayer1.GetComponent<CharacterProperties>().currentTile);

            tilesToPingPlayer1SpyCommand.Add(CommanderPlayer1.GetComponent<CharacterProperties>().currentTile);
            tilesToPingPlayer2SpyCommand.Add(CommanderPlayer1.GetComponent<CharacterProperties>().currentTile);

        }
        if (!Commander2Found &(
            CommanderPlayer2.GetComponent<CharacterProperties>().currentTile == Spy1Player1.GetComponent<CharacterProperties>().currentTile ||
            CommanderPlayer2.GetComponent<CharacterProperties>().currentTile == Spy2Player1.GetComponent<CharacterProperties>().currentTile ||
            CommanderPlayer2.GetComponent<CharacterProperties>().currentTile == Spy3Player1.GetComponent<CharacterProperties>().currentTile
            ))
        {
            Commander2Found = true;
            tilesToPingPlayer2.Add(CommanderPlayer2.GetComponent<CharacterProperties>().currentTile);
            tilesToPingPlayer1.Add(CommanderPlayer2.GetComponent<CharacterProperties>().currentTile);

            tilesToPingPlayer1SpyCommand.Add(CommanderPlayer2.GetComponent<CharacterProperties>().currentTile);
            tilesToPingPlayer2SpyCommand.Add(CommanderPlayer2.GetComponent<CharacterProperties>().currentTile);

        }



    }

    private void DrawPingListPlayer1()
    {
        if (currentFrameFirstInTurn1 || Commander2Found)
        {
            DeletePings(PingsPlayer1);
            if (tilesToPingPlayer1.Count > 0) 
            {
                foreach (var tile in tilesToPingPlayer1)
                {
                    GameObject Ping = Instantiate(PrefabPing);
                    Ping.transform.position = tile.transform.position;
                    PingsPlayer1.Add(Ping);
                }

                foreach (var tile in tilesToPingPlayer1SpyCommand)
                {
                    GameObject Ping = Instantiate(PrefabPingSpyComm);
                    Ping.transform.position = tile.transform.position;
                    PingsPlayer1.Add(Ping);
                }



            }
        }
        tilesToPingPlayer1LastFrame = tilesToPingPlayer1;
    }

    private void DrawPingListPlayer2()
    {
        if (currentFrameFirstInTurn2 || Commander1Found)
        {
            DeletePings(PingsPlayer2);
            if (tilesToPingPlayer2.Count > 0)
            {
                foreach (var tile in tilesToPingPlayer2)
                {
                    GameObject Ping = Instantiate(PrefabPing);
                    Ping.transform.position = tile.transform.position;
                    PingsPlayer2.Add(Ping);
                }

                foreach (var tile in tilesToPingPlayer2SpyCommand)
                {
                    GameObject Ping = Instantiate(PrefabPingSpyComm);
                    Ping.transform.position = tile.transform.position;
                    PingsPlayer2.Add(Ping);
                }

            }
        }
        tilesToPingPlayer2LastFrame = tilesToPingPlayer2;
    }


    private void ClearPingListPlayer1()
    {
        if (currentFrameFirstInTurn2)
        {
            DeletePings(PingsPlayer1);
            PingsPlayer1.Clear();
            tilesToPingPlayer1.Clear();
            tilesToPingPlayer1SpyCommand.Clear();
        }

    }


    private void ClearPingListPlayer2()
    {
        if (currentFrameFirstInTurn1)
        {
            DeletePings(PingsPlayer2);
            tilesToPingPlayer2.Clear();
            PingsPlayer2.Clear();
            tilesToPingPlayer2SpyCommand.Clear();
        }
    }
    private void DeletePings(List<GameObject> pingsPlayer)
    {
        if (pingsPlayer.Count > 0)
        {
            for (int i = pingsPlayer.Count - 1; i >= 0; i--)
            {
                GameObject.Destroy(pingsPlayer[i]);
                pingsPlayer.Remove(pingsPlayer[i]);
            }
        }
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


    private void AddMovementPlayer1() //seems fine


    {
        if (currentFrameFirstInTurn1) //assign movement here for player1 in beginning of turn
        {
            //Debug.Log($"first diceroll assigned: C:{commanderDiceResult1}, A:{armyDiceResult1}, S:{spysDiceResult1}");
            MovementPoolSpyPlayer1.MovementPoolCurrent += MovementPoolArmyPlayer1.MovementPoolCurrent + MovementPoolCommanderPlayer1.MovementPoolCurrent + spysDiceResult1;
            MovementPoolCommanderPlayer1.MovementPoolCurrent = commanderDiceResult1;
            MovementPoolArmyPlayer1.MovementPoolCurrent = armyDiceResult1;
        }
    }
    private void AddMovementPlayer2()
    {
        if (currentFrameFirstInTurn2) //assign movement here for player 2 in beginning of turn
        {
            //Debug.Log($"first diceroll assigned: C:{commanderDiceResult2}, A:{armyDiceResult2}, S:{spysDiceResult2}");
            MovementPoolSpyPlayer2.MovementPoolCurrent += MovementPoolArmyPlayer2.MovementPoolCurrent + MovementPoolCommanderPlayer2.MovementPoolCurrent + spysDiceResult2;
            MovementPoolCommanderPlayer2.MovementPoolCurrent = commanderDiceResult2;
            MovementPoolArmyPlayer2.MovementPoolCurrent = armyDiceResult2;
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
