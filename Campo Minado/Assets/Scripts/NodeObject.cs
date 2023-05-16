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

    public NodeObject(Grid<NodeObject> grid, int x, int y, GameObject prefab)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;

        prefab = GameObject.Instantiate(prefab, grid.GetWorldPosition(x, y), Quaternion.identity);

        prefab.transform.localScale = Vector3.one * 10f;
    }

    public void SetNodeType(NodeTypes nodeType)
    {
        if(this.nodeType == NodeTypes.Empty)
        {
            this.nodeType = nodeType;
            if (nodeType == NodeTypes.Mine)
            {
                CreateBomb(x, y);
            }
                   
            grid.OnTriggedChangedValue(x, y);
        }

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
