using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField] private float _openAngle;

    private bool _isOpened;
    
    public void Open()
    {
        if (_isOpened == false)
        {
            transform.Rotate(Vector3.up, _openAngle);
            _isOpened = true;
        }
    }
}