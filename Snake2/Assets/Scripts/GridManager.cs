using System;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] int _gridX = 20, _gridY = 20;
    [SerializeField] SnakeHeadController _snakeHead;
    [SerializeField] Vector2Int _snakeStartPos;
    [SerializeField] GameObject _wallPrefab;
    GameObject[,] _grid;
    public static GridManager Instance;

    public int GridX { get => _gridX;}
    public int GridY { get => _gridY;}

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InitializeGrid();
    }
    void InitializeGrid()
    {
        _grid = new GameObject[_gridX, _gridY];
        for (int i = 0; i < _gridX; i++)
        {
            for (int j = 0; j < _gridY; j++)
            {
                if(i == 0)
                {
                    GameObject newWall= Instantiate(_wallPrefab, new Vector3(i,j,0), this.transform.rotation);
                    AssignToCell(newWall,i,j);
                }
                else if(i==_gridX-1)
                {
                    GameObject newWall = Instantiate(_wallPrefab, new Vector3(i, j, 0), this.transform.rotation);
                    AssignToCell(newWall, i, j);
                }
                else if(j==_gridY-1 && i != 0 && i!=_gridX-1)
                {
                    GameObject newWall = Instantiate(_wallPrefab, new Vector3(i, j, 0), this.transform.rotation);
                    AssignToCell(newWall, i, j);
                }
                else if (j == 0 && i != 0 && i != _gridX - 1)
                {
                    GameObject newWall = Instantiate(_wallPrefab, new Vector3(i, j, 0), this.transform.rotation);
                    AssignToCell(newWall, i, j);
                }

            }
        }
       
    }
    public void RemoveFromCell(Transform gameObj)
    {
        _grid[TransformToPosXY(gameObj).x, TransformToPosXY(gameObj).y] =null;

        UpdateInfo();
    }

    public void AssignToCell(GameObject gameObject, int x, int y)
    {
        if (!_grid[x, y])
        {       
            _grid[x, y] = gameObject;
      
        }
        else
        {
            HandleActions(gameObject, _grid[x,y]);
       
        }

        UpdateInfo();   
    }

    private void HandleActions(GameObject gameObj, GameObject gridObject)
    {
        if(gameObj.tag == "Snake" && gridObject.tag == "Food")
        {
            //Eat Process
            FoodManager.Instance.TeleportToRandomPos();
            SnakeHeadController.Instance.AddTail();
        }
        else if(gameObj.tag == "Snake" && gridObject.tag == "Obstacle")
        {
            //GameOver
            Debug.Log("GameOver!");
        }
        else if(gameObj.tag == "Food" && (gridObject.tag == "Snake" || gridObject.tag == "Obstacle"))
        {
            FoodManager.Instance.TeleportToRandomPos();
        }
    }
    public bool IsCellFull(int x, int y)
    {
        if (_grid[x, y])
            return true;
        else
            return false;
    }
    Vector2Int TransformToPosXY(Transform gameObj)
    {
        Vector2 gameObjPos = gameObj.position;
        int x = Mathf.RoundToInt(gameObjPos.x);
        int y = Mathf.RoundToInt(gameObjPos.y);
        Vector2Int xy = new Vector2Int(x, y);
        return xy;
    }
    private void UpdateInfo()
    {
        for (int i = 0; i < _gridX; i++)
        {
            for (int j = 0; j < _gridY; j++)
            {
                if (_grid[i, j])
                {
    

                       // Debug.Log("On x: " + i + " On y: " + j + _grid[i,j]);
                    
                    
                }
            }
        }
    }
    //void UpdateNewPosTransform(Transform gameObj)
    //{
    //    for (int i = 0; i < _gridX; i++)
    //    {
    //        for (int j = 0; j < _gridY; j++)
    //        {
    //            if (_grid[i,j] == gameObj.)
    //            {
    //                _grid[i, j] = null;
    //            }
    //        }
    //    }
    //    Vector2 gameObjPos = gameObj.position;
    //    int x = Mathf.RoundToInt(gameObjPos.x);
    //    int y = Mathf.RoundToInt(gameObjPos.y);
    //    _grid[x, y] = gameObj.gameObject;
    //}
    //void SpawnSnake()
    //{
    //    Instantiate(_snakeHead, _snakeStartPos, this.transform.rotation);
    //}

}
