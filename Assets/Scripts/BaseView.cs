using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BaseView : MonoBehaviour
{
    [SerializeField] private Base _base;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _base.ResourceChanged += OnResourceChanged;
    }

    private void OnDisable()
    {
        _base.ResourceChanged -= OnResourceChanged;
    }

    private void OnResourceChanged(int value)
    {
        _text.text = Convert.ToString(value);
    }
}
