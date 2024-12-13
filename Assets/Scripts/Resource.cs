using UnityEngine;

public class Resource : MonoBehaviour
{
    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }
}
