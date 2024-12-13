using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private LayerMask _resourceLayer;
    [SerializeField] private Vector3 _scanBoxSize;
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private float _scanDelay;

    private Queue<Bot> _freeBots;
    private List<Resource> _uncollectedResource;

    private WaitForSeconds _wait;

    private int _resourceCount;

    public event Action<int> ResourceChanged;

    private void Awake()
    {
        _freeBots = new();
        _uncollectedResource = new();
        _wait = new(_scanDelay);
        _resourceCount = 0;
    }

    private void OnEnable()
    {
        foreach (var bot in _bots)
        {
            _freeBots.Enqueue(bot);
            bot.CameBack += OnBotCameBack;
        }

        StartCoroutine(ScanDelay());
    }

    private void OnDisable()
    {
        foreach (var bot in _bots)
        {
            bot.CameBack -= OnBotCameBack;
        }
    }

    private void Update()
    {
        TrySendBotToResource();
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, _scanBoxSize);
    }

    private void Scan()
    {
        Vector3 boxCenter = Vector3.zero;
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, _scanBoxSize, Quaternion.identity, _resourceLayer);

        foreach (var hitCollider in hitColliders)
        {
            hitCollider.TryGetComponent(out Resource resource);
            _uncollectedResource.Add(resource);
        }
    }

    private void TrySendBotToResource()
    {
        if (_freeBots.Count == 0 || _uncollectedResource.Count == 0)
        {
            return;
        }

        _freeBots.Dequeue().GoToResource(_uncollectedResource[0]);
        _uncollectedResource.RemoveAt(0);
    }

    private void OnBotCameBack(Bot bot, Resource resource)
    {
        Destroy(resource.gameObject);
        _freeBots.Enqueue(bot);
        ResourceChanged?.Invoke(++_resourceCount);
    }

    private IEnumerator ScanDelay()
    {
        while (enabled)
        {
            Scan();
            yield return _wait;
        }
    }
}
