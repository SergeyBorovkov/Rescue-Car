using UnityEngine;

public class RoadElement : MonoBehaviour
{    
    [SerializeField] private int _chance;
    [SerializeField] private int _maxChance;
    [SerializeField] private int _maxRandomAngleOnY;
    [SerializeField] private float _leftPositionXOffset;
    [SerializeField] private float _rightPositionXOffset;

    public float LeftPositionXOffset => _leftPositionXOffset;
    public float RightPositionXOffset => _rightPositionXOffset;
    public int Chance => _chance;
    public int MaxChance => _maxChance;

    private void Start()
    {
        if (_maxRandomAngleOnY > 0)
            RandomRotateOnY();
    }

    private void RandomRotateOnY()
    {
        int randomAngle = Random.Range(0, _maxRandomAngleOnY);

        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, randomAngle, transform.eulerAngles.z);
    }
}