using UnityEngine;
using System.Collections.Generic;

public class ResourceUIManager : MonoBehaviour
{
    [Header("Инвентарь игрока")]
    public PlayerInventory inventory;

    [Header("UI настройки")]
    public Transform uiParent;           // Родительский объект для UI слотов
    public GameObject resourceUIPrefab;  // Префаб UI слота с ResourceUI и TextMeshProUGUI

    private List<ResourceUI> uiSlots = new List<ResourceUI>();

    void Start()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory не назначен на ResourceUIManager");
            return;
        }

        if (resourceUIPrefab == null)
        {
            Debug.LogError("ResourceUIPrefab не назначен на ResourceUIManager");
            return;
        }

        if (uiParent == null)
        {
            Debug.LogError("uiParent не назначен на ResourceUIManager");
            return;
        }

        CreateUISlots();
    }

    private void CreateUISlots()
    {
        float yOffset = 0f; // начальное смещение
        float yStep = 30f;  // шаг смещения для каждого нового слота

        foreach (var res in inventory.allResources)
        {
            GameObject go = Instantiate(resourceUIPrefab, uiParent);
            go.name = "UI_" + res.name;

            RectTransform rt = go.GetComponent<RectTransform>();
            if (rt != null)
                rt.anchoredPosition = new Vector2(0f, -yOffset);

            ResourceUI ui = go.GetComponent<ResourceUI>();
            if (ui != null)
            {
                ui.inventory = inventory;
                ui.resource = res;
                uiSlots.Add(ui);
            }

            yOffset += yStep; // сдвиг для следующего слота
        }
    }
}
