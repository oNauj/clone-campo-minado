using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Tilemap
{
    Grid<NodeObject> grid;
    public Tilemap(int width, int height, float cellSize, Vector3 originPosition, GameObject prefab)
    {
        grid = new Grid<NodeObject>(width, height, cellSize, originPosition, (Grid<NodeObject> g, int x, int y) => new NodeObject(g, x, y, prefab));
     
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
    public string GetTilemapType(Vector3 worldPosition)
    {
        return grid.GetValue(worldPosition).ToString();
    }
    

}
