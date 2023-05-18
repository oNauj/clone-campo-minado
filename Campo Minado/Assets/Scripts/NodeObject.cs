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

    public PrefabVisual prefabVisualObject;

    public NodeObject(Grid<NodeObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        
    }

    public void SetNodeType(NodeTypes nodeType)
    {
        if(this.nodeType == NodeTypes.Empty)
        {
            this.nodeType = nodeType;
           
            if (nodeType == NodeTypes.Mine)
            {
                CreateBomb(x, y);
                prefabVisualObject.SetActiveMine();
            }
                   
            grid.OnTriggedChangedValue(x, y);
        }

    }

    public void ShowNode()
    {
        prefabVisualObject.SetActiveNode(false);

        if(nodeType == NodeTypes.Empty) { FloodingReveal(); }
        if (nodeType == NodeTypes .Mine) { }
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
    }

    public NodeTypes GetTilemapSprite()
    {
        return nodeType;
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
