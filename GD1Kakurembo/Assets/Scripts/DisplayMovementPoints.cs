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
                MovementLeftOfUnit.text = $"Commander Movement Points: {MovementPointManager.commanderMovementPool1}\nArmy Movement Points: {MovementPointManager.armyMovementPool1}\nSpies Movement Points: {MovementPointManager.spysMovementPool1}";
                break;
            case PlayerSelector.Player2:
                MovementLeftOfUnit.text = $"Commander Movement Points: {MovementPointManager.commanderMovementPool2}\nArmy Movement Points: {MovementPointManager.armyMovementPool2}\nSpies Movement Points: {MovementPointManager.spysMovementPool2}";
                break;
        }
    }
}
