using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Resource _resource;
    private Vector2 _startPosition;

    private bool _inBase = true;

    public event Action<Bot, Resource> CameBack;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _startPosition = new Vector2(transform.position.x, transform.position.z);    
    }

    private void Update()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.z);

        if (currentPosition == _startPosition && _inBase == false)
        {
            _inBase = true;
            CameBack?.Invoke(this, _resource);
        }

        if (currentPosition != _startPosition)
        {
            _inBase = false;
        }

        if (_resource == null)
        {
            return;
        }

        Vector2 resourcePosition = new Vector2(_resource.transform.position.x, _resource.transform.position.z);

        if (currentPosition == resourcePosition)
        {
            _resource.SetParent(transform);
            GoToBase();
        }
    }

    public void GoToResource(Resource resource)
    {
        _resource = resource;
        _agent.SetDestination(resource.transform.position);
    }

    private void GoToBase()
    {
        _agent.SetDestination(new Vector3(_startPosition.x, 0f, _startPosition.y));
    }
}
