using UnityEngine;

public class RoadElementDeactivator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<RoadElement>())
            other.gameObject.SetActive(false);        
    }   
}