using System.Collections.Generic;
using UnityEngine;

public class BaseScaner : MonoBehaviour
{
    [SerializeField] private Vector3 _scanBoxSize;
    [SerializeField] private LayerMask _resourceLayer;

    public void Scan(UncollectedResources uncollectedResources)
    {
        Vector3 boxCenter = Vector3.zero;
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, _scanBoxSize, Quaternion.identity, _resourceLayer);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Resource resource) == false)
            {
                continue;
            }

            if (uncollectedResources.Spawned.Contains(resource) == false && uncollectedResources.InTransit.Contains(resource) == false)
            {
                uncollectedResources.ResourceSpawned(resource);
            }
        }
    }
}