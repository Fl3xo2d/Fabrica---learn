using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public ResourceData resourceData;   // Настраиваемый тип ресурса

    private Transform player;
    private PlayerInventory playerInventory;
    private bool canBeCollected = false;

    void Start()
    {
        // Находим игрока
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerInventory = playerObj.GetComponent<PlayerInventory>();
        }

        // Случайный вылет при спавне
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDir = new Vector3(
                Random.Range(-1f, 1f),
                0.5f,
                Random.Range(-1f, 1f)
            ).normalized;

            float force = Random.Range(1.5f, 3f);
            rb.AddForce(randomDir * force, ForceMode.Impulse);
        }
    }

    public void EnableMagnetDelayed(float delay)
    {
        Invoke(nameof(EnableMagnet), delay);
    }

    void EnableMagnet()
    {
        canBeCollected = true;
    }

    public bool CanBeCollected()
    {
        return canBeCollected;
    }

    public int GetAmount()
    {
        return resourceData != null ? resourceData.value : 1;
    }

    public Sprite GetIcon()
    {
        return resourceData != null ? resourceData.icon : null;
    }

    public string GetName()
    {
        return resourceData != null ? resourceData.resourceName : "Unknown";
    }
}
