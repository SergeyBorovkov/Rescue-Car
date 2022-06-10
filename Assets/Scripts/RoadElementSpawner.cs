using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoadElementSpawner : RoadElementPool
{
    [SerializeField] private List<RoadElement> _elements;
    [SerializeField] private List<Transform> _elementsTransforms;
    [SerializeField] private Transform _car;

    [SerializeField] LayerMask _layer;
    [SerializeField] private int _lengthOnZ;

    [SerializeField] float _elementDistance;
    [SerializeField] private int _leftGenerationPointOnX;
    [SerializeField] private int _rightGenerationPointOnX;
        
    private int _zStart;
    private int _zEnd;
    private int tempZ;
    private int _fillingOffsetOnZ;

    private void Start()
    {
        InitializePool();

        _zStart = 0;

        _zEnd = _lengthOnZ;

        _fillingOffsetOnZ = _lengthOnZ;

        FillingZ(_zStart, _zEnd + _fillingOffsetOnZ);
    }

    private void Update()
    {
        tempZ = Mathf.RoundToInt(_car.transform.position.z);

        if (tempZ % _lengthOnZ == 0 && tempZ != _zStart)
        {
            _zStart = tempZ;

            _zEnd = _zStart + _lengthOnZ;

            FillingZ(_zStart + _fillingOffsetOnZ, _zEnd + _fillingOffsetOnZ);            
        }
    }

    private void FillingZ(int zStart, int zEnd)
    {
        for (int z = zStart; z <= zEnd; z++)
        {
            for (int x = _leftGenerationPointOnX; x <= _rightGenerationPointOnX; x++)
            {
                TryGenerateElements(new Vector3Int(x, 0, z));
            }
        }
    }

    private void TryGenerateElements(Vector3Int position)
    {
        var randomElement = GetRandom();
        
        if (randomElement == null)
            return;       

        var obstaclePositionY = randomElement.transform.position.y;

        float generationPositionOnX = position.x;

        if (generationPositionOnX < _leftGenerationPointOnX + randomElement.LeftPositionXOffset)
            generationPositionOnX = _leftGenerationPointOnX + randomElement.LeftPositionXOffset;

        if (generationPositionOnX > _rightGenerationPointOnX + randomElement.RightPositionXOffset)
            generationPositionOnX = _rightGenerationPointOnX + randomElement.RightPositionXOffset;        

        var correctedGenerationPosition = new Vector3(generationPositionOnX, obstaclePositionY, position.z);
        
        if (Physics.CheckSphere(correctedGenerationPosition, _elementDistance, _layer) == false)
        {
            randomElement.transform.position = correctedGenerationPosition;
            randomElement.gameObject.SetActive(true);
        }
    }

    private void InitializePool()
    {
        foreach (var element in _elements)
        {
            var parent = TryGetElementTransform(element);

            if (parent == null)
            {
                print("Pool Initializing Error. Set Correct Transforms");
                return;
            }

            Initialize(element, parent);
        }        
    }

    private RoadElement GetRandom()
    {
        foreach (var element in _elements)
        {
            var random = UnityEngine.Random.Range(0, element.MaxChance);

            if (element.Chance > random)
            {
                if (TryGetElement(element.name, out RoadElement poolObstacle))                                    
                    return poolObstacle;                
            }
        }

        return null;
    }

    private Transform TryGetElementTransform(RoadElement element)
    {
        string elementName = element.gameObject.name;

        return _elementsTransforms.FirstOrDefault(element => element.name == elementName);
    }
}