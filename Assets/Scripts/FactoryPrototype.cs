using UnityEngine;
using System.Collections;

public class FactoryPrototype : MonoBehaviour
{
    public ResourceData requiredResource;       // ресурс, который фабрика принимает
    public Transform spawnPoint;                // точка спавна продукта
    public float interactionRadius = 3f;        // радиус для взаимодействия с игроком
    public float processingTime = 2f;           // время переработки
    public GameObject productPrefab;            // префаб продукта

    private bool isProcessing = false;

    void Update()
    {
        if (!isProcessing)
        {
            TryCollectFromPlayer();
        }
    }

    void TryCollectFromPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null) return;

        if (Vector3.Distance(transform.position, playerObj.transform.position) > interactionRadius)
            return;

        PlayerInventory inventory = playerObj.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        if (inventory.GetAmount(requiredResource) > 0)
        {
            // создаём визуальную копию ресурса над игроком
            GameObject resObj = Instantiate(requiredResource.prefab, playerObj.transform.position + Vector3.up, Quaternion.identity);
            ResourcePickup pickup = resObj.GetComponent<ResourcePickup>();
            if (pickup != null)
            {
                // тянем ресурс к фабрике
                pickup.StartMagnet(transform, inventory, requiredResource);
            }

            StartCoroutine(ProcessResource());
        }
    }

    IEnumerator ProcessResource()
    {
        isProcessing = true;
        yield return new WaitForSeconds(processingTime);
        SpawnProduct();
        isProcessing = false;
    }

    void SpawnProduct()
    {
        if (productPrefab != null && spawnPoint != null)
        {
            Instantiate(productPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
