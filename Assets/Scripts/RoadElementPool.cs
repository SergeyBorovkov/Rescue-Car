using System.Collections.Generic;
using UnityEngine;

public class RoadElementPool : MonoBehaviour
{    
    [SerializeField] int _capacity;
        
    private List<RoadElement> _pool = new List<RoadElement>();

    protected void Initialize(RoadElement element, Transform parent)
    {    
        for (int i = 0; i < _capacity; i++)
        {
            RoadElement spawned = Instantiate(element, parent);
            spawned.Deactivate();
            spawned.name = parent.name;

            _pool.Add(spawned);
        }
    }

    protected bool TryGetElement(string elementName, out RoadElement result)
    {
        result = _pool.Find(o => o.name == elementName && o.gameObject.activeSelf == false);

        return result != null;
    }
}