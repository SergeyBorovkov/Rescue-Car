using UnityEngine;

public class RotationFreezer : MonoBehaviour
{
    [SerializeField] private CarMover _car;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;

    private Quaternion _initialRotation;
    
    private void Start()
    {
        _initialRotation = transform.localRotation;        
    }

    private void Update()
    {
        transform.localRotation = _initialRotation;

        transform.position = new Vector3(_car.transform.position.x + _xOffset, _car.transform.position.y + _yOffset, _car.transform.position.z);        
    }
}