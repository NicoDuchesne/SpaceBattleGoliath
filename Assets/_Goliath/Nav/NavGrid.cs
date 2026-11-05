using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell
{
    public int x, y;
    public Vector2 worldPos;
    public bool crossable;
    public int cost;
}

public struct AStarInfos
{
    public Cell parentCell;
    public int cost;
    public int heuristic;
}

public class NavGrid : MonoBehaviour
{
    [SerializeField] private string _leftWallName = "LeftWall";
    [SerializeField] private string _rightWallName = "RightWall";
    [SerializeField] private string _topWallName = "TopWall";
    [SerializeField] private string _botWallName = "BottomWall";
    [SerializeField] private float _cellSize = 0.5f;
    [SerializeField] private string[] _obstacleTags;
    
    private BoxCollider2D _leftWall;
    private BoxCollider2D _rightWall;
    private BoxCollider2D _topWall;
    private BoxCollider2D _botWall;
    

    private Rect _playableArea;
    private Cell[,] _grid;
    private int _gridHeight;
    private int _gridWidth;

    //Testing
    [SerializeField] private string _spaceShip1Name = "SpaceShip1";
    [SerializeField] private string _spaceShip2Name = "SpaceShip2";
    private Transform _spaceShip1;
    private Transform _spaceShip2;
    private List<Vector2> _path;
    
    private void Awake()
    {
        _leftWall = GameObject.Find(_leftWallName).GetComponent<BoxCollider2D>();
        _rightWall = GameObject.Find(_rightWallName).GetComponent<BoxCollider2D>();
        _topWall = GameObject.Find(_topWallName).GetComponent<BoxCollider2D>();
        _botWall = GameObject.Find(_botWallName).GetComponent<BoxCollider2D>();
        _spaceShip1 = GameObject.Find(_spaceShip1Name).GetComponent<Transform>();
        _spaceShip2 = GameObject.Find(_spaceShip2Name).GetComponent<Transform>();
        BuildRect();
        BuildGrid();
    }

    private void Start()
    {
    }

    private void Update()
    {
        UpdateGrid();
        
        Cell startPath = WorldPosToCrossableCell(new Vector2(_spaceShip1.position.x, _spaceShip1.position.y));
        Cell endPath = WorldPosToCrossableCell(new Vector2(_spaceShip2.position.x, _spaceShip2.position.y));
        _path = Astar(startPath, endPath);
    }

    private void BuildRect()
    {
        float minX = _leftWall.bounds.max.x;
        float maxX = _rightWall.bounds.min.x;
        float minY = _botWall.bounds.max.y;
        float maxY = _topWall.bounds.min.y;

        float distX = maxX - minX;
        float distY = maxY - minY;
        
        minY = maxY - (distY - (distY % _cellSize)); 
        maxX = minX + (distX - (distX % _cellSize));
        
        _playableArea = Rect.MinMaxRect(minX, minY, maxX, maxY);
    }

    private void BuildGrid()
    {
        _gridHeight = Mathf.CeilToInt(_playableArea.height / _cellSize);
        _gridWidth = Mathf.CeilToInt(_playableArea.width / _cellSize);
        _grid = new Cell[_gridWidth, _gridHeight];
        
        Vector2 cellBox = Vector2.one * _cellSize * 0.99f;
            
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                
                Vector2 center = new Vector2(_playableArea.xMin + (x + 0.5f) * _cellSize, _playableArea.yMax - (y + 0.5f) * _cellSize);
                bool isCrossable = true;
                
                Collider2D[] hits = Physics2D.OverlapBoxAll(center, cellBox, 0f);
                foreach (Collider2D col in hits)
                {
                    foreach (string obstacleTag in _obstacleTags)
                    {
                        if (col.CompareTag(obstacleTag))
                        {
                            isCrossable = false;
                            break;
                        }
                        
                    }
                }

                if (isCrossable)
                {
                    _grid[x, y] = new Cell { x = x, y = y, worldPos = center, crossable = isCrossable, cost = 1};
                }
                else
                {
                    _grid[x, y] = new Cell { x = x, y = y, worldPos = center, crossable = isCrossable, cost = 999 };
                }
                
            }
        }
    }

    private void UpdateGrid()
    {
        Vector2 cellBox = Vector2.one * _cellSize * 0.99f;
            
        for (int x = 0; x < _gridWidth; x++)
        {
            for (int y = 0; y < _gridHeight; y++)
            {
                Cell c =  _grid[x, y];
                bool isCrossable = true;
                
                Collider2D[] hits = Physics2D.OverlapBoxAll(c.worldPos, cellBox, 0f);
                foreach (Collider2D col in hits)
                {
                    foreach (string obstacleTag in _obstacleTags)
                    {
                        if (col.CompareTag(obstacleTag))
                        {
                            isCrossable = false;
                            break;
                        }
                        
                    }
                }

                if (c.crossable && !isCrossable)
                {
                    c.crossable = false;
                    c.cost = 999;
                }

                if (!c.crossable && isCrossable)
                {
                    c.crossable = true;
                    c.cost = 1;
                }
                
            }
        }
    }

    public Cell WorldPosToCell(Vector2 pos)
    {
        int x = Mathf.Clamp(Mathf.FloorToInt((pos.x - _playableArea.xMin) / _cellSize), 0, _gridWidth - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt((_playableArea.yMax - pos.y) / _cellSize), 0, _gridHeight - 1);
        return _grid[x, y];
    }

    public Cell WorldPosToCrossableCell(Vector2 pos)
    {
        Cell cell = WorldPosToCell(pos);
        if (cell.crossable) return cell;
        
        float minDist = float.MaxValue;
        Cell nearest = cell;
        foreach (Cell c in GetNeighbours(cell))
        {
            float dist = Vector2.Distance(pos, c.worldPos);
            if (minDist > dist)
            {
                minDist = dist;
                nearest = c;
            }
        }

        return nearest; 
    }
    
    public List<Cell> GetNeighbours(Cell c)
    {
        List<Cell> result = new List<Cell>();

        for (int j = -1; j < 2; j++)
        {
            int x = c.x + j;
            
            for (int k = -1; k < 2; k++)
            {
                int y = c.y + k;
                
                if (j == 0 && k == 0) continue;
                if (x < 0 || y < 0 || x >= _gridWidth || y >= _gridHeight) continue;
                if (!_grid[x, y].crossable) continue;
                if (Mathf.Abs(j) == 1 && Mathf.Abs(k) == 1)
                {
                    if (x+k < 0 || y+j < 0 || x+k >= _gridWidth || y+j >= _gridHeight) continue;
                    if(!_grid[x+k, y].crossable) continue;
                    if(!_grid[x, y+j].crossable) continue;
                }
                
                result.Add(_grid[x, y]);
            }
        }
        
        return result;
    }

     public List<Vector2> Astar(Cell startCell, Cell endCell)
     {
         List<Cell> lockedCells = new List<Cell>();
         Dictionary<Cell, AStarInfos> dictInfos = new Dictionary<Cell, AStarInfos>();
    
         AStarInfos startInfo = new AStarInfos { parentCell = null, cost = 0, heuristic = Chebyshev(startCell, endCell) };
         dictInfos[startCell] =  startInfo;
         Cell currentCell =  startCell;
    
         while (currentCell != endCell && currentCell != null)
         {
             foreach (Cell c in GetNeighbours(currentCell))
             {
                 if (lockedCells.Contains(c)) continue;
                 int actualCost = dictInfos[currentCell].cost + c.cost;

                 if (dictInfos.ContainsKey(c))
                 {
                     if (dictInfos[c].cost > actualCost)
                     {
                         dictInfos[c] = new AStarInfos { parentCell = currentCell, cost = actualCost, heuristic = actualCost + Manhattan(c, endCell) };
                     }
                 }
                 else
                 {
                     dictInfos[c] = new AStarInfos { parentCell = currentCell, cost = actualCost, heuristic = actualCost + Manhattan(c, endCell) };
                 }
             }
             lockedCells.Add(currentCell);

             currentCell = null;
             int minHeuristic = Int32.MaxValue;

             foreach (Cell key in dictInfos.Keys)
             {
                 AStarInfos value = dictInfos[key];

                 if (lockedCells.Contains(key)) continue;

                 if (minHeuristic > value.heuristic)
                 {
                     minHeuristic = value.heuristic;
                     currentCell = key;
                 }
             }
         }
         
         List<Vector2> result = new List<Vector2>();
         if (currentCell == null)
         {
             result.Add(endCell.worldPos);
         }
         else
         {
             Cell currentPathCell = endCell;
             result.Add(endCell.worldPos);

             while (currentPathCell != startCell)
             {
                 result.Add(dictInfos[currentPathCell].parentCell.worldPos);
                 currentPathCell = dictInfos[currentPathCell].parentCell;
             }
             result.Reverse();
         }
         
         return result;
    }
    
    private int Chebyshev(Cell firstCell, Cell secondCell)
    {
        return Mathf.Max(Mathf.Abs(secondCell.x - firstCell.x), Mathf.Abs(secondCell.y - firstCell.y));
    }
    
    private int Manhattan(Cell firstCell, Cell secondCell)
    {
        return Mathf.Abs(secondCell.x - firstCell.x) + Mathf.Abs(secondCell.y - firstCell.y);
    }
    
    private void OnDrawGizmos()
    {
        if (_leftWall == null || _rightWall == null || _topWall == null || _botWall == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_playableArea.center, _playableArea.size);
        
        if (_grid == null) return;
        foreach (Cell n in _grid)
        {
            Gizmos.color = n.crossable ? new Color(1, 1, 1, 0.25f) : new Color(1, 0, 0, 0.5f);
            Gizmos.DrawCube(n.worldPos, Vector3.one * _cellSize * 0.9f);
            Gizmos.DrawSphere(n.worldPos, 0.05f);
        }
        
        if (_path == null) return;
        Gizmos.color = Color.green;
        foreach (Vector2 v in _path)
        {
            Gizmos.DrawCube(v, Vector3.one * _cellSize * 0.9f);
        }
    }
    
}
