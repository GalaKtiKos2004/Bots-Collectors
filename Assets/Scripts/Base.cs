using System;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private List<Bot> _bots;

    private int _resourceCount;
    private Queue<Bot> _freeBots;
    private Queue<Resource> _uncollectedResource;

    public event Action<int> ResourceChanged;

    private void Awake()
    {
        _resourceCount = 0;
        _freeBots = new();
        _uncollectedResource = new();
    }

    private void OnEnable()
    {
        foreach (var point in _spawnPoints)
        {
            point.ResourceSpawned += OnResourceSpawned;
        }

        foreach (var bot in _bots)
        {
            _freeBots.Enqueue(bot);
            bot.CameBack += OnBotCameBack;
        }
    }

    private void OnDisable()
    {
        foreach (var point in _spawnPoints)
        {
            point.ResourceSpawned -= OnResourceSpawned;
        }

        foreach (var bot in _bots)
        {
            bot.CameBack -= OnBotCameBack;
        }
    }

    private void Update()
    {
        TrySendBotToResource();
    }

    private void OnResourceSpawned(Resource resource)
    {
        _uncollectedResource.Enqueue(resource);

    }

    private void TrySendBotToResource()
    {
        if (_freeBots.Count == 0 || _uncollectedResource.Count == 0)
        {
            return;
        }

        _freeBots.Dequeue().GoToResource(_uncollectedResource.Dequeue());
    }

    private void OnBotCameBack(Bot bot, Resource resource)
    {
        Destroy(resource.gameObject);
        _freeBots.Enqueue(bot);
        ResourceChanged?.Invoke(++_resourceCount);
    }
}
