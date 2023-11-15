using System.Collections.Generic;
using UnityEngine;

public class Grid2 : MonoBehaviour//, IGraph
{
    [SerializeField] int _width = 1, _height = 1;
    [SerializeField] float offset = 0.1f;
    Node2[,] _grid;
    [SerializeField] Node2 _nodePrefab;
    GameManager2 gm;

    private void Start()
    {
        GenerateGrid();
        gm = GameManager2.instance;
    }


    public void PaintNodesWhite(Node2 start, Node2 end)
    {
        foreach (var item in _grid)
        {
            if (item == start || item == end) continue;
            if (item.isBlocked) continue;
            item.ChangeColor(item.Cost > 1 ? item.costColor : Color.white);
        }
    }



    private void GenerateGrid()
    {
        _grid = new Node2[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                var node = Instantiate(_nodePrefab);
                _grid[x, y] = node;
                node.transform.position = new Vector3(x + x * offset, y + y * offset, 0);
                node.transform.SetParent(transform);

                node.Initialize(new Coordinates2(x, y), this);
            }
        }

    }

    //Tengo una matriz, recibo un gameobject, como hago para conseguir la posicion en la matriz del gameobject pasado.
    public List<Node2> GetNeighbors(Coordinates2 coordinates)
    {
        var neighbors = new List<Node2>();
        if (coordinates.y + 1 < _height) neighbors.Add(_grid[coordinates.x, coordinates.y + 1]);
        if (coordinates.x + 1 < _width) neighbors.Add(_grid[coordinates.x + 1, coordinates.y]);
        if (coordinates.y - 1 >= 0) neighbors.Add(_grid[coordinates.x, coordinates.y - 1]);
        if (coordinates.x - 1 >= 0) neighbors.Add(_grid[coordinates.x - 1, coordinates.y]);


        return neighbors;
    }

    public Coordinates2 GetObjectCoordinates(GameObject current)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                if (_grid[x, y] == current)
                {
                    return new Coordinates2(x, y);
                }
            }
        }
        return default;
    }

}


