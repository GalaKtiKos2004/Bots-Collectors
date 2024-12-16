using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstraper : MonoBehaviour
{
    [SerializeField] private Base _base;
    [SerializeField] private BaseView _baseView;

    private void Awake()
    {
        Wallet wallet = new();
        _base.Init(wallet);
        _baseView.Init(wallet);
    }
}
