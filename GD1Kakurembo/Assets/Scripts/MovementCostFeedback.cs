using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static DisplayMovementPoints;

public class MovementCostFeedback : MonoBehaviour
{
    public TextMeshProUGUI MovementCostFeedbackText;
    public PlayerSelector attachedPlayer;
    public UnitSelector UnitselectorPlayer1;
    public UnitSelector UnitselectorPlayer2;





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
                if (UnitselectorPlayer1.clickedObject && UnitselectorPlayer1.aimingTile && UnitselectorPlayer1.aimingTile != UnitselectorPlayer1.currentTileSelectedUnit && UnitselectorPlayer1.CheckIfTileAdjacentGO(UnitselectorPlayer1.aimingTile))
                {
                    MovementCostFeedbackText.text = UnitselectorPlayer1.movementCostCurrentAimingTile.ToString();
                    MovementCostFeedbackText.rectTransform.position = Input.mousePosition;
                    if (UnitselectorPlayer1.CheckIfEnoughMovementCostLeft())
                    {
                        MovementCostFeedbackText.color = Color.green;
                    }
                    else
                    {
                        MovementCostFeedbackText.color = Color.red;
                    }
                }
                else
                {
                    MovementCostFeedbackText.rectTransform.position = new Vector3(-1000,-1000,-1000);
                }
                break;
            case PlayerSelector.Player2:
                if (UnitselectorPlayer2.clickedObject && UnitselectorPlayer2.aimingTile && UnitselectorPlayer2.aimingTile != UnitselectorPlayer2.currentTileSelectedUnit && UnitselectorPlayer2.CheckIfTileAdjacentGO(UnitselectorPlayer2.aimingTile))
                {
                    MovementCostFeedbackText.text = UnitselectorPlayer2.movementCostCurrentAimingTile.ToString();
                    MovementCostFeedbackText.rectTransform.position = Input.mousePosition;
                    if (UnitselectorPlayer2.CheckIfEnoughMovementCostLeft())
                    {
                        MovementCostFeedbackText.color = Color.green;
                    }
                    else
                    {
                        MovementCostFeedbackText.color = Color.red;
                    }
                }
                else
                {
                    MovementCostFeedbackText.rectTransform.position = new Vector3(-1000, -1000, -1000);
                }
                break;
        }
    }



}
