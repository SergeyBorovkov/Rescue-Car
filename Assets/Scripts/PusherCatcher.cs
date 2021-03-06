using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PusherCatcher : MonoBehaviour
{
    [SerializeField] private Pusher _pusherPrefab;
    [SerializeField] private List<Transform> _pushPoints;
    [SerializeField] private Door _leftDoor;
    [SerializeField] private Door _rightDoor;
    [SerializeField] private Transform _leftDoorPoint;
    [SerializeField] private Transform _rightDoorPoint;

    public event Action <int> PushersCountChanged;

    private Pusher _defaultPusher;    

    public int MaxPushers => _pushPoints.Count;

    private void Start()
    {
        OpenDoor(_leftDoorPoint);

        _defaultPusher = Instantiate(_pusherPrefab, _leftDoorPoint.position, Quaternion.identity);

        _defaultPusher.IsDefault = true;

        Catch(_defaultPusher, _leftDoorPoint);        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.activeSelf == true && other.TryGetComponent<Pusher>(out Pusher pusher) && TryGetFreePushPoint(out Transform freePoint))
        {
            if (pusher.IsPushing == false && pusher.CanPush)
            {
                OpenDoor(freePoint);

                Catch(pusher, freePoint);
            }
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.activeSelf == true && other.GetComponent<Pusher>())        
            UpdatePushersCount();        
    }

    private void UpdatePushersCount()
    {
        int count = _pushPoints.FindAll(p => p.childCount > 0).Count;

        PushersCountChanged?.Invoke(count);        
    }

    private bool TryGetFreePushPoint(out Transform freePoint)
    {
        freePoint = _pushPoints.FirstOrDefault(p => p.childCount == 0);

        return freePoint != null;
    }

    private void Catch(Pusher pusher, Transform point)
    {
        pusher.IsPushing = true;

        pusher.transform.position = point.position;
        pusher.transform.rotation = point.transform.rotation;
        pusher.transform.SetParent(point);

        pusher.PlayPushAnimation();

        UpdatePushersCount();
    }

    private void OpenDoor(Transform doorPoint)
    {                
        if (doorPoint == _leftDoorPoint)        
            _leftDoor.Open();

        if (doorPoint == _rightDoorPoint)
            _rightDoor.Open();
    }
}