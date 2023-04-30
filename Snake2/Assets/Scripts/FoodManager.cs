using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    [SerializeField] GameObject _foodPrefab;
    GameObject _currentFood;
    public static FoodManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SpawnToRandomPos();
    }
    void SpawnToRandomPos()
    {
        int x = GridManager.Instance.GridX;
        int y = GridManager.Instance.GridY;
        Vector3Int randomPos = new Vector3Int(Random.Range(0, x), Random.Range(0, y), 0);
        _currentFood = Instantiate(_foodPrefab, randomPos, this.transform.rotation);
        GridManager.Instance.AssignToCell(_foodPrefab, randomPos.x, randomPos.y);

    }
    public void TeleportToRandomPos()
    {
        GridManager.Instance.RemoveFromCell(_foodPrefab.transform);
        int x = GridManager.Instance.GridX;
        int y = GridManager.Instance.GridY;
        Vector3Int randomPos = new Vector3Int(Random.Range(0, x), Random.Range(0, y), 0);
        if(GridManager.Instance.IsCellFull(randomPos.x, randomPos.y))
        {
            TeleportToRandomPos();
        }
        else
        {
            _currentFood.transform.position = randomPos;
            GridManager.Instance.AssignToCell(_foodPrefab, randomPos.x, randomPos.y);
        }
    }
}
