using System;
using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Resource _prefab;
    [SerializeField] private float _delay;

    public event Action<Resource> ResourceSpawned;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new(_delay);
    }

    private void Start()
    {
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        while (enabled)
        {
            yield return _wait;

            var resource = Instantiate(_prefab, transform.position, Quaternion.identity);
            ResourceSpawned?.Invoke(resource);
        }
    }
}
