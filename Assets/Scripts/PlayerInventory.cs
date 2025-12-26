using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ResourceAmount
{
    public ResourceData resource;
    public int amount;
}

public class PlayerInventory : MonoBehaviour
{
    [Header("Все ресурсы")]
    public List<ResourceData> allResources;

    [Header("Текущие ресурсы")]
    public List<ResourceAmount> resourceList = new List<ResourceAmount>();

    private Dictionary<int, int> resources = new Dictionary<int, int>(); // ключ — ResourceData.id

    void Awake()
    {
        resources.Clear();

        foreach (var res in allResources)
        {
            if (res != null && !resources.ContainsKey(res.id))
                resources[res.id] = 0;
        }

        SyncList();
    }

    public void AddResource(ResourceData resource, int amount = 1)
    {
        if (resource == null) return;

        if (resources.ContainsKey(resource.id))
            resources[resource.id] += amount;
        else
            resources[resource.id] = amount;

        SyncList();
    }

    public void RemoveResource(ResourceData resource, int amount = 1)
    {
        if (resource == null || !resources.ContainsKey(resource.id)) return;

        resources[resource.id] -= amount;
        if (resources[resource.id] < 0) resources[resource.id] = 0;

        SyncList();
    }

    public int GetAmount(ResourceData resource)
    {
        if (resource == null) return 0;
        resources.TryGetValue(resource.id, out int amount);
        return amount;
    }

    private void SyncList()
    {
        resourceList.Clear();
        foreach (var kvp in resources)
        {
            ResourceData res = FindResourceByID(kvp.Key);
            if (res != null)
            {
                resourceList.Add(new ResourceAmount
                {
                    resource = res,
                    amount = kvp.Value
                });
            }
        }
    }

    private ResourceData FindResourceByID(int id)
    {
        foreach (var r in allResources)
        {
            if (r != null && r.id == id)
                return r;
        }
        return null;
    }
}
