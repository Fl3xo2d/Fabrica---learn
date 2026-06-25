using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    public ResourceData resource; // сам ресурс
    public int amount = 1;

    [HideInInspector] public bool canBeCollected = false;
    [HideInInspector] public bool isMagnetActive = false;

    private Transform magnetTarget;
    private PlayerInventory magnetInventory; // если тянет игрок
    private ResourceData resourceToCollect;  // ресурс для цели (игрок или фабрика)
    public float baseSpeed = 4f;
    public float acceleration = 8f;
    private float currentSpeed = 0f;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomDir = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0.5f, UnityEngine.Random.Range(-1f, 1f)).normalized;
            float force = UnityEngine.Random.Range(1.5f, 3f);
            rb.AddForce(randomDir * force, ForceMode.Impulse);
        }
    }

    // Запуск магнитного притяжения
    public void StartMagnet(Transform target, PlayerInventory inventory, ResourceData res)
    {
        magnetTarget = target;
        magnetInventory = inventory; // если null — значит фабрика
        resourceToCollect = res;
        canBeCollected = true;
        isMagnetActive = true;
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        if (isMagnetActive && magnetTarget != null)
        {
            currentSpeed += acceleration * Time.deltaTime;

            transform.position = Vector3.MoveTowards(
                transform.position,
                magnetTarget.position,
                currentSpeed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, magnetTarget.position) < 0.3f)
            {
                Collect();
            }
        }
    }

    void Collect()
    {
        if (magnetInventory != null && resourceToCollect != null)
        {
            // Игрок собирает ресурс → увеличиваем его инвентарь
            magnetInventory.AddResource(resourceToCollect, amount);
        }
        // Иначе это фабрика — ресурс уже списан у игрока и учитывается в стеше фабрики

        Destroy(gameObject);
    }
}
