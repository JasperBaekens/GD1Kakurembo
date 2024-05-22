using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static CharacterProperties;
using static System.Net.Mime.MediaTypeNames;

public class DisplayMovementPoints : MonoBehaviour
{
    public TextMeshProUGUI MovementPoolSpyUI;
    public TextMeshProUGUI MovementPoolCommanderUI;
    public TextMeshProUGUI MovementPoolArmyUI;

    public MovementPointManager MovementPointManager;
    public PlayerSelector attachedPlayer;


    //movementpointfix
    public MovementPool MovementPoolSpyPlayer1;
    public MovementPool MovementPoolSpyPlayer2;
    public MovementPool MovementPoolArmyPlayer1;
    public MovementPool MovementPoolArmyPlayer2;
    public MovementPool MovementPoolCommanderPlayer1;
    public MovementPool MovementPoolCommanderPlayer2;





    public enum PlayerSelector
    {
        Player1,
        Player2
    }

    

    public void Update()
    {
        //color currentUnit
        switch (attachedPlayer)
        {
            case PlayerSelector.Player1:
                MovementPoolCommanderUI.color = new Vector4(0.7607843f, 0.308064f, 0.2470588f, 1);
                MovementPoolArmyUI.color = new Vector4(0.7607843f, 0.308064f, 0.2470588f, 1);
                MovementPoolSpyUI.color = new Vector4(0.7607843f, 0.308064f, 0.2470588f, 1);

                if (MovementPointManager.UnitselectorPlayer1.clickedObject != null)
                {
                    if (MovementPointManager.UnitselectorPlayer1.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Commander)
                    {
                        MovementPoolCommanderUI.color = new Vector4(0.7250586f, 0.5554566f, 0.8616352f, 1);
                    }
                    else if (MovementPointManager.UnitselectorPlayer1.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Army)
                    {
                        MovementPoolArmyUI.color = new Vector4(0.7250586f, 0.5554566f, 0.8616352f, 1);
                    }
                    else if (MovementPointManager.UnitselectorPlayer1.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Spy)
                    {
                        MovementPoolSpyUI.color = new Vector4(0.7250586f, 0.5554566f, 0.8616352f, 1);
                    }
                }

                break;
            case PlayerSelector.Player2:
                MovementPoolCommanderUI.color = new Vector4(0.334065f, 0.4513354f, 0.7987421f, 1);
                MovementPoolArmyUI.color = new Vector4(0.334065f, 0.4513354f, 0.7987421f, 1);
                MovementPoolSpyUI.color = new Vector4(0.334065f, 0.4513354f, 0.7987421f, 1);

                if (MovementPointManager.UnitselectorPlayer2.clickedObject != null)
                {

                    if (MovementPointManager.UnitselectorPlayer2.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Commander)
                    {
                        MovementPoolCommanderUI.color = new Vector4(0.7250586f, 0.5554566f, 0.8616352f, 1);
                    }
                    else if (MovementPointManager.UnitselectorPlayer2.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Army)
                    {
                        MovementPoolArmyUI.color = new Vector4(0.7250586f, 0.5554566f, 0.8616352f, 1);
                    }
                    else if (MovementPointManager.UnitselectorPlayer2.clickedObject.GetComponent<CharacterProperties>().unitMovementpoolType == UnitMovementPool.Spy)
                    {
                        MovementPoolSpyUI.color = new Vector4(0.7250586f, 0.5554566f, 0.8616352f, 1);
                    }
                }
                break;
        }



        //display
        switch (attachedPlayer)
        {
            case PlayerSelector.Player1:
                MovementPoolCommanderUI.text = $"General Movement Points: {MovementPoolCommanderPlayer1.MovementPoolCurrent}";
                MovementPoolArmyUI.text = $"Army Movement Points: {MovementPoolArmyPlayer1.MovementPoolCurrent}";
                MovementPoolSpyUI.text = $"Spies Movement Points: {MovementPoolSpyPlayer1.MovementPoolCurrent}";
                break;
            case PlayerSelector.Player2:
                MovementPoolCommanderUI.text = $"General Movement Points: {MovementPoolCommanderPlayer2.MovementPoolCurrent}";
                MovementPoolArmyUI.text = $"Army Movement Points: {MovementPoolArmyPlayer2.MovementPoolCurrent}";
                MovementPoolSpyUI.text = $"Spies Movement Points: {MovementPoolSpyPlayer2.MovementPoolCurrent}";
                break;
        }
    }
}
