using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResourceUI : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public Transform resourceListParent;   // родитель для UI элементов
    public GameObject resourceUIPrefab;    // префаб с иконкой и текстом

    private Dictionary<ResourceData, Text> resourceTexts = new Dictionary<ResourceData, Text>();

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        // Получаем словарь через публичный метод
        Dictionary<ResourceData, int> allResources = playerInventory.GetAllResources();

        foreach (var kv in allResources)
        {
            ResourceData data = kv.Key;
            int amount = kv.Value;

            if (!resourceTexts.ContainsKey(data))
            {
                GameObject go = Instantiate(resourceUIPrefab, resourceListParent);
                go.name = data.resourceName;

                // предположим, что в префабе есть Image и Text
                Image icon = go.transform.Find("Icon").GetComponent<Image>();
                Text text = go.transform.Find("Amount").GetComponent<Text>();

                icon.sprite = data.icon;
                text.text = amount.ToString();

                resourceTexts[data] = text;
            }
            else
            {
                resourceTexts[data].text = amount.ToString();
            }
        }
    }
}
