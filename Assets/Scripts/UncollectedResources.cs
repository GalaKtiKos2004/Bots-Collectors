using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UncollectedResources : MonoBehaviour
{
    private List<Resource> _spawned;
    private List<Resource> _inTransit;

    private void Awake()
    {
        _spawned = new();
        _inTransit = new();
    }

    public List<Resource> Spawned => _spawned.ToList();
    public List<Resource> InTransit => _inTransit.ToList();

    public void ResourceSpawned(Resource resource)
    {
        _spawned.Add(resource);
    }

    public void TookResource(Resource resource)
    {
        _inTransit.Add(resource);  
    }

    public void RemoveSpawned(Resource resource) => _spawned.Remove(resource);

    public void RemoveTransit(Resource resource) => _inTransit.Remove(resource);
}