using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private Transform _car;
    [SerializeField] private RoadTile _tile;
    [SerializeField] private int _tileCount;
    [SerializeField] private int _tileOffsetOnZ;
    [SerializeField] private int _tilesToMove;

    private List<RoadTile> _road = new List<RoadTile>();
    private RoadTile _tileToMove;
    private Vector3 _newPositon;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;

        for (int i = 0; i < _tileCount; i++)
        {
            var nextPosition = new Vector3(_startPosition.x, _startPosition.y, _startPosition.z + _tileOffsetOnZ * i);

            var roadTile = Instantiate(_tile, nextPosition, Quaternion.identity, transform);            

            _road.Add(roadTile);
        }        
    }

    private void Update()
    {
        if (_car.transform.position.z > GetMinPositionZ() + _tileOffsetOnZ * _tilesToMove)
            MoveRoad();               
    }

    private void MoveRoad()
    {
        _tileToMove = GetTileInMinPositionZ();

        _tileToMove.gameObject.SetActive(false);

        _newPositon = new Vector3(_startPosition.x, _startPosition.y, _tileToMove.transform.position.z + _tileCount * _tileOffsetOnZ);

        _tileToMove.transform.position = _newPositon;

        _tileToMove.gameObject.SetActive(true);
    }

    private float GetMinPositionZ ()
    {
        return _road.Min(tile => tile.transform.position.z);
    }    

    private RoadTile GetTileInMinPositionZ()
    {
        return _road.First(tile => tile.transform.position.z == GetMinPositionZ());
    }
}