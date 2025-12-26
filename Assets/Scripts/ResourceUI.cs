using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [HideInInspector] public PlayerInventory inventory;
    [HideInInspector] public ResourceData resource;
    [HideInInspector] public TextMeshProUGUI text;

    void Start()
    {
        if (inventory != null)
            inventory.OnResourceChanged += UpdateUI;
    }

    void UpdateUI(ResourceData changedResource, int amount)
    {
        if (changedResource != resource) return;
        text.text = amount.ToString();
    }

    void OnDestroy()
    {
        if (inventory != null)
            inventory.OnResourceChanged -= UpdateUI;
    }
}
