using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    [SerializeField] private Transform _car;
    [SerializeField] private BackgroundTile _tile;    
    [SerializeField] private int _tileCountOnZ;
    [SerializeField] private int _tileCountOnX;
    [SerializeField] private int _tileOffsetOnZ;
    [SerializeField] private int _tilesToMove;    

    private List<BackgroundTile> _background = new List<BackgroundTile>();
    private Vector3 _newPositon;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = _tile.transform.position;

        for (int x = -_tileCountOnX; x < _tileCountOnX; x++)
        {
            for (int z = -_tileCountOnZ; z < _tileCountOnZ; z++)
            {
                var nextPosition = new Vector3(_startPosition.x + _tileOffsetOnZ * x, _startPosition.y, _startPosition.z + _tileOffsetOnZ * z);

                var backgroundElement = Instantiate(_tile, nextPosition, Quaternion.identity, transform);

                _background.Add(backgroundElement);
            }
        }
    }

    private void Update()
    {
        if (_car.transform.position.z > GetMinPositionZ() + _tileOffsetOnZ * _tilesToMove)
            MoveBackground();
    }

    private void MoveBackground()
    {
        var tilesToMove = GetElementsInMinPositionZ();        

        foreach (var tile in tilesToMove)
        {
            _newPositon = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.transform.position.z + _tileOffsetOnZ * _tileCountOnZ);

            tile.transform.position = _newPositon;

            tile.Activate();
        }
    }

    private float GetMinPositionZ()
    {
        return _background.Min(element => element.transform.position.z);
    }

    private List<BackgroundTile> GetElementsInMinPositionZ()
    {
        float minZposition = _background.Min(element => element.transform.position.z);

        return _background.FindAll(element => element.transform.position.z == minZposition);
    }
}