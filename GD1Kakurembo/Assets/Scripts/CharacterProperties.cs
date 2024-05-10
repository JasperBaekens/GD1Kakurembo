using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProperties : MonoBehaviour
{
    private Material characterMaterial;
    private Renderer characterRenderer;
    public GameObject currentTile;

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
