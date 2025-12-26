using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [Header("Ссылка на инвентарь и ресурс")]
    public PlayerInventory inventory;
    public ResourceData resource;

    [Header("Текстовый элемент для отображения")]
    public TextMeshProUGUI textField;

    void Start()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory не назначен на " + name);
            return;
        }

        if (textField == null)
        {
            Debug.LogError("TextMeshProUGUI не назначен на " + name);
            return;
        }

        UpdateText();
        // Автообновление каждые 0.1 секунды
        InvokeRepeating(nameof(UpdateText), 0f, 0.1f);
    }

    void OnDisable()
    {
        CancelInvoke(nameof(UpdateText));
    }

    public void UpdateText()
    {
        if (inventory == null || resource == null || textField == null) return;

        int amount = inventory.GetAmount(resource);
        textField.text = amount.ToString();
    }
}
