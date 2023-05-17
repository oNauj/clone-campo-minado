using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class GameHandler : MonoBehaviour
{
    Tilemap tilemap;
    [SerializeField] private int boardSize;
    [SerializeField] private float cellSize;
    [SerializeField] private TilemapVisual tilemapVisual;

    private void Start()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        int sideBoardSize = Mathf.FloorToInt(Mathf.Sqrt(boardSize));

        tilemap = new Tilemap(sideBoardSize, sideBoardSize, cellSize, -new Vector3(1f, 1f, 0f) * (sideBoardSize*sideBoardSize/2));

        tilemap.SetTilemapVisual(tilemapVisual);

    }

    private void Update()
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

        if(Input.GetMouseButtonDown(0))
        {
            tilemap.SetNodeObjectType(mouseWorldPosition, NodeObject.NodeTypes.Mine);
        }
        if(Input.GetMouseButtonDown(1)) 
        {
            tilemap.GetTilemapType(mouseWorldPosition).ShowNode();
        }
    }

}

