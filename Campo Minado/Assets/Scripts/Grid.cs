using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using System;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private TGridObject[,] gridArray;
    private TextMesh[,] debugTextArray;
    private float cellSize;
    private Vector3 originPosition;

    public event EventHandler<OnGridChangedValueEventArgs> OnGridChangedValue;
    public class OnGridChangedValueEventArgs : EventArgs
    {
        public int x; 
        public int y; 
    }


    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        // Calcula o offset para posicionar o TextMesh no centro da célula
        Vector3 offset = Vector3.one * cellSize * 0.5f;
        // Cria um TextMesh para cada célula do grid
       ShowDebug(width, height, offset);


    }

    private void ShowDebug(int width, int height, Vector3 offset)
    {
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                // Desenha linhas para visualizar o grid
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y) + offset, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
            }
        }
        // Desenha as linhas do grid para as bordas direita e superior
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);

        OnGridChangedValue += (object sender, OnGridChangedValueEventArgs e) =>
        {
            debugTextArray[e.x, e.y].text = gridArray[e.x, e.y].ToString();
        };
    }


    public Vector3 GetWorldPosition(int x, int y)
    {
       //Escala o tamanho do vetor de acordo com tamanho da celula.
       return new Vector3(x, y) * cellSize + originPosition;
    }
    private void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellSize);
        y = Mathf.FloorToInt((worldPosition.y - originPosition.y) / cellSize);
    }

    private void SetValue(int x, int y, TGridObject value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            OnGridChangedValue?.Invoke(this, new OnGridChangedValueEventArgs { x = x, y = y });
        }
    }
    public void OnTriggedChangedValue(int x, int y)
    {
        OnGridChangedValue?.Invoke(this, new OnGridChangedValueEventArgs { x = x, y = y });
    }
    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public TGridObject GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }
    public TGridObject GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x , out y);
        return GetValue(x, y);
    }

    public int GetWidth()
    {
        return width;
    }
    public int GetHeight()
    {
        return height;
    }
    public float GetCellSize()
    {
        return cellSize;
    }

}
