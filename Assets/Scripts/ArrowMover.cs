using System;
using System.Collections;
using UnityEngine;

public class ArrowMover : MonoBehaviour
{
    [SerializeField] private CarMover _carMover;
    [SerializeField] private float _maxAngleOnZ;
    [SerializeField] private float _minAngleOnZ;
    [SerializeField] private int _tiltAngle;

    private Action _isMaxAngleAchieved;
    private Coroutine _tiltJob;
    private WaitForSeconds _pause;
    private Vector3 _defaultRotation;    
    private float _currentRotationOnZ;
    private float _angleAmplitude;
    private float _tiltInterval = 0.5f;
    private float _speedIndexForTilt = 0.95f;

    private void OnEnable()
    {
        _isMaxAngleAchieved += OnMaxAngleAchieved;
    }

    private void OnDisable()
    {
        _isMaxAngleAchieved -= OnMaxAngleAchieved;
    }

    private void Start()
    {
        _defaultRotation = transform.localRotation.eulerAngles;        
        _angleAmplitude = _maxAngleOnZ - _minAngleOnZ;
        _pause = new WaitForSeconds(_tiltInterval);
    }

    private void Update()
    {       
        Move();        
    }

    private void OnMaxAngleAchieved()
    {
        if (_tiltJob == null)
        {
            _tiltJob = StartCoroutine(TiltArrow());
        }
        else
        {
            StopCoroutine(_tiltJob);
            _tiltJob = null;
        }
    }

    private void Move()
    {
        float currentSpeedIndex = (_carMover.CurrentSpeed - _carMover.StartSpeed) / (_carMover.MaxSpeed - _carMover.StartSpeed);

        _currentRotationOnZ = _minAngleOnZ + _angleAmplitude * currentSpeedIndex;

        transform.localRotation = Quaternion.Euler(_defaultRotation.x, _defaultRotation.y, _currentRotationOnZ);
        
        if (currentSpeedIndex > _speedIndexForTilt)                    
            _isMaxAngleAchieved?.Invoke();        
    }

    private void Tilt()
    {
        var tiltAngle = UnityEngine.Random.Range(_maxAngleOnZ - _tiltAngle, _maxAngleOnZ + _tiltAngle);        
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, tiltAngle);
    }

    private IEnumerator TiltArrow()
    {        
        Tilt();            

        yield return _pause;        
    }
}