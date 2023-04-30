
using System.Collections.Generic;
using UnityEngine;

public class SnakeHeadController : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] GameObject _snakeBodyPrefab;
    float _moveTimeCounter;
    float _moveTimePeriod;

    public static SnakeHeadController Instance;
    Vector3 _nextDirection;
    List<GameObject> _snakeBodyList = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _moveTimePeriod = 10 / _moveSpeed;
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.y);
        GridManager.Instance.AssignToCell(this.gameObject,x,y);
    }
    private void Update()
    {
        HandleInput();
        _moveTimeCounter -= Time.deltaTime;
        if (_moveTimeCounter < 0)
        {
            _moveTimeCounter = _moveTimePeriod;
            HandleBody();
            GridManager.Instance.RemoveFromCell(this.transform);
            this.transform.position += _nextDirection;
            int x = Mathf.RoundToInt(transform.position.x);
            int y = Mathf.RoundToInt(transform.position.y);
            GridManager.Instance.AssignToCell(this.gameObject, x, y);
        }
    }

    private void HandleBody()
    {
        if(_snakeBodyList.Count == 0) return;
        for (int i = _snakeBodyList.Count - 1; i > 0 ; i--)
        {
            if(i==_snakeBodyList.Count-1)
            {
                GridManager.Instance.RemoveFromCell(_snakeBodyList[i].transform);  //?
            }
            _snakeBodyList[i].transform.position = _snakeBodyList[i - 1].transform.position;
            int x = Mathf.RoundToInt(_snakeBodyList[i].transform.position.x);
            int y = Mathf.RoundToInt(_snakeBodyList[i].transform.position.y);
            GridManager.Instance.RemoveFromCell(_snakeBodyList[i - 1].transform);
            GridManager.Instance.AssignToCell(_snakeBodyList[i].gameObject, x, y);
        }
    }
    public void AddTail()
    {
        if (_snakeBodyList.Count == 0)
            _snakeBodyList.Add(this.gameObject);
        GameObject newTail = Instantiate(_snakeBodyPrefab, _snakeBodyList[_snakeBodyList.Count -1].transform.position, this.transform.rotation);
        _snakeBodyList.Add(newTail);

    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _nextDirection = Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _nextDirection = Vector2.down;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _nextDirection = Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _nextDirection = Vector2.left;
        }
    }
}

