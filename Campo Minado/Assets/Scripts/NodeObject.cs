using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeObject 
{
    public enum NodeTypes
    {
        Empty,
        Mine,
        Flag
    }

    private Grid<NodeObject> grid;
    private int x;
    private int y;
    private int emptyNumber = 0;

    private NodeTypes nodeType;
    private NodeTypes nodeLastType;

    public PrefabVisual prefabVisualObject;

    public NodeObject(Grid<NodeObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        
    }

    public void SetNodeType(NodeTypes nodeType)
    {
        if(prefabVisualObject.GetNodeisActive())
        {
            if (nodeType == this.nodeType )
            {
                if (nodeType == NodeTypes.Flag)
                {
                    GameHandler.Instance.flagSize++;
                }
                this.nodeType = nodeLastType;
                nodeType = this.nodeType;


            }
            else
            {
                nodeLastType = this.nodeType;
                this.nodeType = nodeType;
            }

            if (nodeType == NodeTypes.Empty)
            {
                prefabVisualObject.SetActiveFlag(false);
            }
            if (nodeType == NodeTypes.Flag)
            {
                if (GameHandler.Instance.flagSize > 0)
                {
                    prefabVisualObject.SetActiveFlag(true);
                    GameHandler.Instance.flagSize--;
                }
                else
                {
                    nodeType = nodeLastType;
                    this.nodeType = nodeType;
                }
            }
            if (nodeType == NodeTypes.Mine)
            {

                prefabVisualObject.SetActiveFlag(false);
                if(nodeLastType != NodeTypes.Mine)
                CreateBomb(x, y);
                prefabVisualObject.SetActiveMine();
            }

            grid.OnTriggedChangedValue(x, y);
        }

   
    }

    public void ShowNode()
    {

        if(nodeType != NodeTypes.Flag)
        prefabVisualObject.SetActiveNode(false);

        if(nodeType == NodeTypes.Empty) { FloodingReveal(); }
        if (nodeType == NodeTypes .Mine) { }

        grid.OnTriggedChangedValue(x, y);
    }


    private void CreateBomb(int x, int y)
    {
        grid.GetValue(x + 1, y + 1)?.AddNumber(1);
        grid.GetValue(x - 1, y - 1)?.AddNumber(1);
        grid.GetValue(x + 1, y)?.AddNumber(1);
        grid.GetValue(x - 1, y)?.AddNumber(1);
        grid.GetValue(x, y + 1)?.AddNumber(1);
        grid.GetValue(x, y - 1)?.AddNumber(1);
        grid.GetValue(x - 1, y + 1)?.AddNumber(1);
        grid.GetValue(x + 1, y - 1)?.AddNumber(1);


        
    }
    private void GetNearbyNodes(int x, int y)
    { 
        grid.GetValue(x + 1, y + 1)?.FloodingReveal();
        grid.GetValue(x - 1, y - 1)?.FloodingReveal();
        grid.GetValue(x + 1, y)?.FloodingReveal();
        grid.GetValue(x - 1, y)?.FloodingReveal();
        grid.GetValue(x, y + 1)?.FloodingReveal();
        grid.GetValue(x, y - 1)?.FloodingReveal();
        grid.GetValue(x - 1, y + 1)?.FloodingReveal();
        grid.GetValue(x + 1, y - 1)?.FloodingReveal();
    }

    private void AddNumber(int number)
    {
        if(nodeType != NodeTypes.Mine)
        {
            this.emptyNumber++;
            prefabVisualObject.SetNumber(emptyNumber);


            switch (emptyNumber)
            {
                case 1: prefabVisualObject.SetNumberColor(Color.blue);
                    break;
                case 2: prefabVisualObject.SetNumberColor(Color.green);
                    break;
                case 3: prefabVisualObject.SetNumberColor(Color.red);
                    break;
                default:
                    float RandomColor = UnityEngine.Random.Range(0, 1f);
                    Debug.Log(RandomColor);
                    prefabVisualObject.SetNumberColor(new Color(RandomColor, 0f , RandomColor));
                    break;
            }
            grid.OnTriggedChangedValue(x, y);

        }

    }
    public void FloodingReveal()
    {
        if (emptyNumber == 0)
        {
            emptyNumber = -1000;
            ShowNode();
            GetNearbyNodes(x, y);
        }
        grid.OnTriggedChangedValue(x, y);
    }

    public NodeTypes GetTilemapSprite()
    {
        return nodeType;
    }
    public NodeTypes GetLastNodeType()
    {
        return nodeLastType;
    }

    public override string ToString()
    {
        if(nodeType == NodeTypes.Empty)
        {
            return emptyNumber.ToString();
        }
        return nodeType.ToString();
    }

}
