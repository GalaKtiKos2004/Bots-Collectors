using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BaseView : MonoBehaviour
{
    private Wallet _wallet;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnDisable()
    {
        _wallet.ResourceChanged -= OnResourceChanged;
    }

    public void Init(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.ResourceChanged += OnResourceChanged;
    }

    private void OnResourceChanged(int value)
    {
        _text.text = Convert.ToString(value);
    }
}
