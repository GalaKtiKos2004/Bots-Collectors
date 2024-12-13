using System;
using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Resource _prefab;

    public void SpawnResource()
    {
        Instantiate(_prefab, transform.position, Quaternion.identity);
    }
}
