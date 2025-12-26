using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    [Header("Resource")]
    public ResourceData resource;
    public int amount = 1;

    [HideInInspector]
    public bool canBeCollected = false;

    [HideInInspector]
    public Transform target; // игрок, к которому летим

    public float baseSpeed = 4f;
    public float acceleration = 8f;

    private float currentSpeed;

    void Update()
    {
        if (!canBeCollected || target == null)
            return;

        currentSpeed += acceleration * Time.deltaTime;

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            currentSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            Collect();
        }
    }

    public void StartMagnet(Transform player)
    {
        target = player;
        canBeCollected = true;
        currentSpeed = baseSpeed;
    }

    void Collect()
    {
        var inventory = target.GetComponent<PlayerInventory>();
        inventory.AddResource(resource, amount);
        Destroy(gameObject);
    }
}
