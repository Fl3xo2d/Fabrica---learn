using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactoryPrototype : MonoBehaviour
{
    [Header("Настройки фабрики")]
    public ResourceData requiredResource;    // ресурс, который фабрика принимает
    public Transform spawnPoint;             // точка спавна продукта
    public float interactionRadius = 3f;     // радиус для взаимодействия с игроком
    public float processingTime = 3f;        // время переработки одного ресурса
    public GameObject productPrefab;         // префаб продукта
    public int maxStoredResources = 5;       // максимум ресурсов в стеше фабрики
    public float collectInterval = 1f;       // интервал забора ресурса у игрока

    private int storedResources = 0;         // сколько ресурсов уже на фабрике
    private bool isProcessing = false;
    private float lastCollectTime = 0f;

    void Update()
    {
        TryCollectFromPlayer();

        if (!isProcessing && storedResources > 0)
        {
            StartCoroutine(ProcessResource());
        }
    }

    void TryCollectFromPlayer()
    {
        if (storedResources >= maxStoredResources) return;

        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj == null) return;

        if (Vector3.Distance(transform.position, playerObj.transform.position) > interactionRadius)
            return;

        PlayerInventory inventory = playerObj.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        if (Time.time - lastCollectTime < collectInterval) return;

        int playerAmount = inventory.GetAmount(requiredResource);
        if (playerAmount <= 0) return;

        // списываем ресурс из инвентаря игрока
        inventory.RemoveResource(requiredResource, 1);

        // создаём визуальный префаб ресурса у игрока
        GameObject resObj = Instantiate(requiredResource.prefab, playerObj.transform.position + Vector3.up, Quaternion.identity);
        ResourcePickup pickup = resObj.GetComponent<ResourcePickup>();
        if (pickup != null)
        {
            // тянем ресурс к фабрике
            pickup.StartMagnet(transform, null, requiredResource); // null = это фабрика, не игрок
            pickup.amount = 1;
        }

        storedResources += 1;
        lastCollectTime = Time.time;
    }

    IEnumerator ProcessResource()
    {
        isProcessing = true;

        while (storedResources > 0)
        {
            yield return new WaitForSeconds(processingTime);

            SpawnProduct();
            storedResources -= 1;
        }

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
