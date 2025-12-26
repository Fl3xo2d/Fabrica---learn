using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    [Header("Магнитные настройки")]
    public float magnetRadius = 3f;       // радиус магнитного действия
    public float baseSpeed = 4f;          // базовая скорость притяжения
    public float acceleration = 8f;       // ускорение притяжения

    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        if (playerInventory == null)
            Debug.LogError("PlayerInventory не найден на игроке!");
    }

    void Update()
    {
        // Находим все ресурсы вокруг игрока
        Collider[] hits = Physics.OverlapSphere(transform.position, magnetRadius);
        foreach (var hit in hits)
        {
            ResourcePickup pickup = hit.GetComponent<ResourcePickup>();
            if (pickup != null && !pickup.canBeCollected)
            {
                // Проверяем, есть ли ресурс у игрока, который нужно притянуть
                ResourceData resourceToCollect = pickup.resource;
                if (resourceToCollect != null)
                {
                    // Запускаем магнит с нужными параметрами
                    pickup.StartMagnet(transform, playerInventory, resourceToCollect);

                    // Устанавливаем начальные скорости для плавного притяжения
                    pickup.baseSpeed = baseSpeed;
                    pickup.acceleration = acceleration;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}
