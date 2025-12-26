using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ResourceData, int> resources = new();

    // событие для UI
    public event Action<ResourceData, int> OnResourceChanged;

    public void AddResource(ResourceData data, int amount)
    {
        if (!resources.ContainsKey(data))
            resources[data] = 0;

        resources[data] += amount;

        // уведомляем UI
        OnResourceChanged?.Invoke(data, resources[data]);
    }

    public int GetAmount(ResourceData data)
    {
        return resources.TryGetValue(data, out int value) ? value : 0;
    }
}
