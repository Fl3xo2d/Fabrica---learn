using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ResourceData, int> resources = new Dictionary<ResourceData, int>();

    public void AddResource(ResourceData data)
    {
        if (data == null) return;

        if (!resources.ContainsKey(data))
            resources[data] = 0;

        resources[data] += data.value;
        Debug.Log($"Collected {data.resourceName}: {resources[data]}"); // проверка
    }

    public Dictionary<ResourceData, int> GetAllResources()
    {
        return new Dictionary<ResourceData, int>(resources);
    }
}
