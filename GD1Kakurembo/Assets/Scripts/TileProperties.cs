using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    private Material tileMaterial;
    private Renderer tileRenderer;

    public TileType tileType;
    public enum TileType
    {
        BaseTile,
        WaterTile,
        ForestTile,
        VillageTile,
        RoadTile
    }



    private void Awake()
    {
        tileRenderer = GetComponent(typeof(Renderer)) as Renderer;
    }


    private void Update()
    {

        tileMaterial = tileRenderer.material;

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.gameObject.CompareTag("Base"))
                r.material = tileMaterial;
        }
    }
}
