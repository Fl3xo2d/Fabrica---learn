using UnityEngine;
using TMPro;

public class ResourceUIManager : MonoBehaviour
{
    [Header("References")]
    public PlayerInventory inventory;
    public ResourceDatabase database;
    public GameObject resourceUIPrefab; // prefab с Icon + TextMeshProUGUI

    [Header("Layout Settings")]
    public float spacing = 30f; // расстояние между блоками

    void Start()
    {
        if (inventory == null || database == null || resourceUIPrefab == null)
        {
            Debug.LogError("ResourceUIManager: не назначены ссылки!");
            return;
        }

        float yOffset = 0f;

        foreach (var res in database.allResources)
        {
            GameObject go = Instantiate(resourceUIPrefab, transform);
            go.name = res.name;

            // позиционируем вертикально
            go.transform.localPosition = new Vector3(0, -yOffset, 0);
            yOffset += spacing;

            // находим текст в prefab
            var text = go.GetComponentInChildren<TextMeshProUGUI>();
            if (text == null)
            {
                Debug.LogError("ResourceUIPrefab не содержит TextMeshProUGUI!");
                continue;
            }

            // добавляем компонент ResourceUI
            var ui = go.AddComponent<ResourceUI>();
            ui.inventory = inventory;
            ui.resource = res;
            ui.text = text;

            // обновление текста сразу
            text.text = inventory.GetAmount(res).ToString();
        }
    }
}
