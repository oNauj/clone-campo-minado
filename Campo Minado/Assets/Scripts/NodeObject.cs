using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject 
{
    public enum NodeTypes
    {
        Empty,
        Mine
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

    private void AddNumber(int number)
    {
        this.emptyNumber++;
        prefabVisualObject.SetNumber(emptyNumber);
        grid.OnTriggedChangedValue(x, y);
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
