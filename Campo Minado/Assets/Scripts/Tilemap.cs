using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Tilemap
{
    Grid<NodeObject> grid;
    private int width, height;

    private List<NodeObject> nodesBombs;
    public Tilemap(int width, int height, float cellSize, Vector3 originPosition)
    {
        grid = new Grid<NodeObject>(width, height, cellSize, originPosition, (Grid<NodeObject> g, int x, int y) => new NodeObject(g, x, y));
        this.width = width;
        this.height = height;
        nodesBombs = new List<NodeObject>();

        GameHandler.Instance.OnExplode += Instance_OnExplode;

    }

    private void Instance_OnExplode(object sender, GameHandler.OnExplodeEventArgs e)
    {
        foreach (var item in nodesBombs)
        {
            Debug.Log("Explodiu");
            if (item != e.nodeObject)
            {
                
                item.prefabVisualObject.SetActiveNode(false);
            }
           
        }
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

    public void CreateBombs(int numberBombs)
    {
        for (int n = 0; n < numberBombs; ++n)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            NodeObject nodeObject = grid.GetValue(x, y);
            if (nodeObject.GetTilemapSprite() != NodeObject.NodeTypes.Mine && nodeObject != null)
            {
                nodeObject.SetNodeType(NodeObject.NodeTypes.Mine);
                nodesBombs.Add(nodeObject);
            }
            else
            {
                n--;
            }

        }
    }



    public Grid<NodeObject> GetGrid() { return grid; }


}
