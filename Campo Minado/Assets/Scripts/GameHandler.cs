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

    [SerializeField] private int totalBombsSize;

    public static GameHandler Instance { get;  set;}

    public  event EventHandler<OnExplodeEventArgs> OnExplode;
    public class OnExplodeEventArgs : EventArgs
    {
        public NodeObject nodeObject;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        ChangeTilemapPositionInCenter();
        tilemap.SetTilemapVisual(tilemapVisual);
    }
    private void Start()
    {

        tilemap.CreateBombs(totalBombsSize);

    }


    private void ChangeTilemapPositionInCenter()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        int sideBoardSize = Mathf.FloorToInt(Mathf.Sqrt(boardSize));

        tilemap = new Tilemap(sideBoardSize, sideBoardSize, cellSize, cameraPosition - new Vector3(1f, 1f, 0f) * (sideBoardSize * sideBoardSize / 2));
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

        if(Input.GetMouseButtonDown(0))
        {
            NodeObject nodeObject = tilemap.GetTilemapType(mouseWorldPosition);
            nodeObject?.ShowNode();
            if (nodeObject != null && nodeObject.GetTilemapSprite() == NodeObject.NodeTypes.Mine)
            {
                OnExplode?.Invoke(this, new OnExplodeEventArgs() {nodeObject = nodeObject });
            }
        }
        if(Input.GetMouseButtonDown(1)) 
        {
          
        }
    }

}

