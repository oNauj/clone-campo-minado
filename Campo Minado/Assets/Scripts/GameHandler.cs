using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    Tilemap tilemap;
    [SerializeField] private int boardSize;
    [SerializeField] private float cellSize;
    [SerializeField] private float proportionBoard;
    [SerializeField] private TilemapVisual tilemapVisual;

    [SerializeField] private int totalBombsSize;
    [SerializeField] private int initFlagSize = 10;

    int sideBoardSize;

    public int flagSize = 0;
    public static GameHandler Instance { get;  set;}

    public  event EventHandler<OnExplodeEventArgs> OnExplode;
    public  event EventHandler<OnAddFlagEventArgs> OnAddFlag;
    public event EventHandler OnVictory;
    public event EventHandler OnFail;
    public event EventHandler<OnAddTimerEventArgs> OnAddTimer;

    float currentTimer;

    public class OnExplodeEventArgs : EventArgs
    {
        public NodeObject nodeObject;
    }
    public class OnAddFlagEventArgs : EventArgs
    {
        public int flagSize;
    }
    public class OnAddTimerEventArgs : EventArgs
    {
        public int minutes, seconds;
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

        sideBoardSize = Mathf.FloorToInt(Mathf.Sqrt(boardSize));
        ChangeTilemapPositionInCenter();
        tilemap.SetTilemapVisual(tilemapVisual);
        tilemap.GetGrid().OnGridChangedValue += Grid_OnGridChangedValue;

        


    }

    private void UpdateTimer(float currentTimer)
    {
    
        this.currentTimer += currentTimer;
          TimeSpan time = TimeSpan.FromSeconds(this.currentTimer);

        OnAddTimer.Invoke(this, new OnAddTimerEventArgs() { minutes = time.Minutes, seconds = time.Seconds});

    
    }

    private void Grid_OnGridChangedValue(object sender, Grid<NodeObject>.OnGridChangedValueEventArgs e)
    {
        int quantidade = 0;
        for (int x = 0; x < sideBoardSize; x++)
        {
            for (int y = 0; y < sideBoardSize; y++)
            {
                NodeObject nodeObject = tilemap.GetGrid().GetValue(x, y);
                if (nodeObject.prefabVisualObject.GetNodeisActive() == false || nodeObject.GetLastNodeType() == NodeObject.NodeTypes.Mine)
                {
                    quantidade++;
                }
            }
        }

        if (boardSize == quantidade)
        {
            GameIsOverWin();
        }
    }
    private void GameIsOverWin()
    {
        OnVictory?.Invoke(this, EventArgs.Empty);
    }

    private void Start()
    {

        tilemap.CreateBombs(totalBombsSize);
        flagSize = initFlagSize;
        currentTimer = 0;
        OnAddFlag?.Invoke(this, new OnAddFlagEventArgs() { flagSize = flagSize });
     

    }


    private void ChangeTilemapPositionInCenter()
    {

        Camera.main.orthographicSize = boardSize/2 + proportionBoard;


        tilemap = new Tilemap(sideBoardSize, sideBoardSize, cellSize, new Vector3(-sideBoardSize * cellSize/2, -sideBoardSize * cellSize / 2));
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
        UpdateTimer(Time.deltaTime);


        if(Input.GetMouseButtonDown(0))
        {
            NodeObject nodeObject = tilemap.GetTilemapType(mouseWorldPosition);
            nodeObject?.ShowNode();
            if (nodeObject != null && nodeObject.GetTilemapSprite() == NodeObject.NodeTypes.Mine)
            {
                OnExplode?.Invoke(this, new OnExplodeEventArgs() {nodeObject = nodeObject });
                bool failEvent = tilemap.failEvent;
                StartCoroutine(tilemap.ExplodeNodes(nodeObject, 0.3f));

                Debug.Log(failEvent);

            }
        }
        if(Input.GetMouseButtonDown(1)) 
        {

                NodeObject nodeObject = tilemap.GetTilemapType(mouseWorldPosition);
                nodeObject?.SetNodeType(NodeObject.NodeTypes.Flag);

                OnAddFlag.Invoke(this, new OnAddFlagEventArgs() { flagSize = flagSize});
       
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            for (int x = 0; x < sideBoardSize; x++)
            {
                for (int y = 0; y < sideBoardSize; y++)
                {
                    NodeObject nodeObject = tilemap.GetGrid().GetValue(x, y);
                    if (nodeObject.prefabVisualObject.GetNodeisActive())
                    {
                       
                        nodeObject.ShowNode();
                    }
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
          ResetGame();
            
        }
    }
    public void OnVictoryFuction()
    {

            OnFail?.Invoke(this, EventArgs.Empty);
      
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

}

