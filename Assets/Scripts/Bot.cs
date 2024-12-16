using System;
using UnityEngine;

[RequireComponent(typeof(BotMover))]
public class Bot : MonoBehaviour
{
    private Resource _resource;
    private BotMover _mover;

    private Vector2 _startPosition;

    private bool _inBase = true;

    public event Action<Bot, Resource> CameBack;

    private void Awake()
    {
        _mover = GetComponent<BotMover>();
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
            _mover.GoToPoint(new Vector3(_startPosition.x, 0f, _startPosition.y));
        }
    }

    public void GoToResource(Resource resource)
    {
        _resource = resource;
        _mover.GoToPoint(resource.transform.position);
    }
}
