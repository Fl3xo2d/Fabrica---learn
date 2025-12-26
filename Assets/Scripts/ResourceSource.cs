using UnityEngine;

public class ResourceSource : MonoBehaviour
{
    [Header("Resource Settings")]
    public int maxAmount = 5;
    private int currentAmount;

    public float respawnInterval = 1f; // восстановление одного ресурса
    private float respawnTimer;

    public GameObject resourcePrefab;

    [Header("Visual Settings")]
    public Transform modelTransform;
    public Vector3 minScale = Vector3.zero;
    public Vector3 maxScale = Vector3.one;

    void Start()
    {
        currentAmount = maxAmount;
        UpdateVisual();
    }

    void Update()
    {
        if (currentAmount < maxAmount)
        {
            respawnTimer += Time.deltaTime;
            if (respawnTimer >= respawnInterval)
            {
                currentAmount++;
                respawnTimer = 0f;
                UpdateVisual();
            }
        }
    }

    // Вызывается, когда игрок бьёт источник
    public void Hit()
    {
        if (currentAmount <= 0) return;

        currentAmount--;
        UpdateVisual();
        SpawnResource();
    }

    void SpawnResource()
    {
        Vector3 spawnPos = transform.position;

        GameObject res = Instantiate(resourcePrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = res.GetComponent<Rigidbody>();
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

        ResourcePickup pickup = res.GetComponent<ResourcePickup>();
        if (pickup != null)
            pickup.EnableMagnetDelayed(0.5f);
    }

    void UpdateVisual()
    {
        if (modelTransform == null) return;

        float t = (float)currentAmount / maxAmount;
        modelTransform.localScale = Vector3.Lerp(minScale, maxScale, t);
    }
}
