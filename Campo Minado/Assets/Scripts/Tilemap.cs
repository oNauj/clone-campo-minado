using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Tilemap
{
    Grid<NodeObject> grid;
    public Tilemap(int width, int height, float cellSize, Vector3 originPosition)
    {
        grid = new Grid<NodeObject>(width, height, cellSize, originPosition, (Grid<NodeObject> g, int x, int y) => new NodeObject(g, x, y));
     
    }

    public void SetNodeObjectType(Vector3 worldPosition, NodeObject.NodeTypes nodeType)
    {
        NodeObject NodeObject = grid.GetValue(worldPosition);
        if (NodeObject != null)
        {
            NodeObject.SetNodeType(nodeType);
        }

    }
    public void SetTilemapVisual(TilemapVisual tilemapVisual)
    {
        tilemapVisual.SetGrid(grid);
    }
    public NodeObject GetTilemapType(Vector3 worldPosition)
    {
        return grid.GetValue(worldPosition);
    }
    

}
