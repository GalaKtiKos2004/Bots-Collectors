using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseScaner))]
[RequireComponent(typeof(UncollectedResources))]
public class Base : MonoBehaviour
{
    [SerializeField] private List<Bot> _bots;
    [SerializeField] private float _scanDelay;

    private Queue<Bot> _freeBots;

    private WaitForSeconds _wait;

    private UncollectedResources _resources;
    private Wallet _wallet;
    private BaseScaner _scaner;

    private void Awake()
    {
        _freeBots = new();
        _wait = new(_scanDelay);
        _resources = GetComponent<UncollectedResources>();
        _scaner = GetComponent<BaseScaner>();
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

    public void Init(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void TrySendBotToResource()
    {
        if (_freeBots.Count == 0 || _resources.Spawned.Count == 0)
        {
            return;
        }

        _freeBots.Dequeue().GoToResource(_resources.Spawned[0]);
        _resources.TookResource(_resources.Spawned[0]);
        _resources.RemoveSpawned(_resources.Spawned[0]);
    }

    private void OnBotCameBack(Bot bot, Resource resource)
    {
        _resources.RemoveSpawned(resource);
        Destroy(resource.gameObject);
        _freeBots.Enqueue(bot);
        _wallet.AddResource();
    }

    private IEnumerator ScanDelay()
    {
        while (enabled)
        {
            _scaner.Scan(_resources);
            yield return _wait;
        }
    }
}