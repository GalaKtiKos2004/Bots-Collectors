using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private float _spawnDelay;

    private WaitForSeconds _wait;

    private void Awake()
    {
        _wait = new(_spawnDelay);
    }

    private void Start()
    {
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        while (enabled)
        {
            _spawnPoints[Random.Range(0, _spawnPoints.Count)].SpawnResource();
            yield return _wait;
        }
    }
}
