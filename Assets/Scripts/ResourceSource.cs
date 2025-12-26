using UnityEngine;

public class ResourceSource : MonoBehaviour
{
    [Header("Resource")]
    public ResourceData resource;
    public GameObject resourcePrefab;

    [Header("Storage")]
    public int maxAmount = 10;
    public int currentAmount = 10;

    [Header("Regeneration")]
    public float regenTime = 2f;

    [Header("Visual")]
    public Transform model;
    private Vector3 baseScale;

    bool isRegenerating;

    void Start()
    {
        baseScale = model.localScale;
        UpdateVisual();
    }

    public void Hit()
    {
        if (currentAmount <= 0)
            return;

        currentAmount--;

        SpawnResource();
        UpdateVisual();

        if (!isRegenerating)
            StartCoroutine(Regenerate());
    }

    void SpawnResource()
    {
        Vector3 randomOffset = Random.insideUnitSphere * 0.5f;
        randomOffset.y = 0;

        GameObject go = Instantiate(
            resourcePrefab,
            transform.position + randomOffset,
            Quaternion.identity
        );

        ResourcePickup pickup = go.GetComponent<ResourcePickup>();
        pickup.resource = resource;
    }

    System.Collections.IEnumerator Regenerate()
    {
        isRegenerating = true;

        while (currentAmount < maxAmount)
        {
            yield return new WaitForSeconds(regenTime);
            currentAmount++;
            UpdateVisual();
        }

        isRegenerating = false;
    }

    void UpdateVisual()
    {
        float t = (float)currentAmount / maxAmount;
        model.localScale = baseScale * Mathf.Clamp01(t);
    }
}
