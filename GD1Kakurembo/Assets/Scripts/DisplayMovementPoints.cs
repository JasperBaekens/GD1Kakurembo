using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMovementPoints : MonoBehaviour
{
    public TextMeshProUGUI MovementLeftOfUnit;
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
        switch (attachedPlayer)
        {
            case PlayerSelector.Player1:
                MovementLeftOfUnit.text = $"Commander Movement Points: {MovementPoolCommanderPlayer1.MovementPoolCurrent}\nArmy Movement Points: {MovementPoolArmyPlayer1.MovementPoolCurrent}\nSpies Movement Points: {MovementPoolSpyPlayer1.MovementPoolCurrent}";
                break;
            case PlayerSelector.Player2:
                MovementLeftOfUnit.text = $"Commander Movement Points: {MovementPoolCommanderPlayer2.MovementPoolCurrent}\nArmy Movement Points: {MovementPoolArmyPlayer2.MovementPoolCurrent}\nSpies Movement Points: {MovementPoolSpyPlayer2.MovementPoolCurrent}";
                break;
        }
    }
}
