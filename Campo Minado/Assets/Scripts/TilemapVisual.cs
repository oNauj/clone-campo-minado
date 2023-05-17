using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEditor;

public class TilemapVisual : MonoBehaviour
{
    private Grid<NodeObject> grid;

    private bool updateMesh;

    [SerializeField] private Transform nodePrefabVisual;


    public void SetGrid(Grid<NodeObject> grid)
    {
        print("Oi");
        this.grid = grid;
        UpdateHeatMapVisual();


    }



    private void UpdateHeatMapVisual()
    {
        foreach (Transform child in transform)
        {
            if (child != null)
                Destroy(child.gameObject);
        }

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {

                Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();

                NodeObject gridObject = grid.GetValue(x, y);
                NodeObject.NodeTypes tilemapSprite = gridObject.GetTilemapSprite();

                Vector3 offset = grid.GetWorldPosition(x, y) + quadSize/2;

                Transform prefabTransform = Instantiate(nodePrefabVisual, offset, Quaternion.identity, transform);
                prefabTransform.localScale = quadSize;
                
                gridObject.prefabVisualObject = prefabTransform.GetComponent<PrefabVisual>();

                gridObject.prefabVisualObject.SetNumber(0);

                gridObject.prefabVisualObject.SetActiveNode(false);       


            }


        }
    }

}
