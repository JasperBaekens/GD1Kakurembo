using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperties : MonoBehaviour
{
    private Material characterMaterial;
    private Renderer characterRenderer;
    public GameObject currentTile;

    public UnitMovementPool unitMovementpoolType;
    public Player unitPlayer;



    //movementpoolfix
    public MovementPool MovementPool;
    
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
        characterRenderer = GetComponent(typeof(Renderer)) as Renderer;
    }


    private void Update()
    {

        characterMaterial = characterRenderer.material;

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            {
	            r.material = characterMaterial;
            }
    }

}
