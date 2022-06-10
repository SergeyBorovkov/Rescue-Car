using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarMover : MonoBehaviour
{
    [SerializeField] private PusherCatchController _catcher;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _turnConstrainIndex;
    [SerializeField] private bool _isNonPlayer;

    private Rigidbody _rigidBody;
    private Vector3 _input;    
    private float _currentSpeed;
    private float _targetSpeed;    
    private float _rigidBodyMoveIndex = 0.01f;
    private float _currentSpeedIncreaseIndex = 8f;

    public float StartSpeed => _startSpeed;
    public float MaxSpeed => _maxSpeed;
    public float CurrentSpeed => _currentSpeed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _currentSpeed = _startSpeed;
    }

    private void OnEnable()
    {
        _catcher.PushersCountChanged += OnPushersCountChanged;
    }

    private void OnDisable()
    {
        _catcher.PushersCountChanged -= OnPushersCountChanged;
    }

    private void OnPushersCountChanged(int currentPushers)
    {
        _targetSpeed = _startSpeed + (float)currentPushers / _catcher.MaxPushers * (_maxSpeed - _startSpeed);
    }

    private void Update()
    {
        GatherInput();
        LookAt();        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        if (_isNonPlayer == false)
            _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        else        
            _input = new Vector3(0, 0, 0);
    }

    private void LookAt()
    {
        if (_input != Vector3.zero && _isNonPlayer == false)
        {                   
            var direction = _input;
            
            var rotation = Quaternion.LookRotation(direction);

            rotation = new Quaternion(rotation.x, rotation.y * _turnConstrainIndex, rotation.z, rotation.w);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _turnSpeed * Time.deltaTime);
        }
    }
    private void Move()
    {        
        _currentSpeed = Mathf.SmoothStep(_currentSpeed, _targetSpeed, Time.deltaTime * _currentSpeedIncreaseIndex);

       _rigidBody.MovePosition(transform.position + transform.forward * _currentSpeed * _rigidBodyMoveIndex);        
    }
}