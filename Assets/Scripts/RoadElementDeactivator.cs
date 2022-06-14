using UnityEngine;

public class RoadElementDeactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<RoadElement>(out RoadElement roadElement))
            roadElement.Deactivate();
    }   
}