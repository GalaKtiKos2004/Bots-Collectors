using System;

public class Wallet
{
    public Wallet()
    {
        ResourcesCount = 0;
    }

    public event Action<int> ResourceChanged;

    public int ResourcesCount { get; private set; }

    public void AddResource()
    {
        ResourcesCount++;
        ResourceChanged?.Invoke(ResourcesCount);
    }
}
