using UnityEngine;

public class PlayerResourceCollector : MonoBehaviour
{
    public float collectRadius = 3f;
    public float basePullSpeed = 3f;
    public float maxPullSpeed = 12f;
    public float magnetDelay = 0.5f;

    private PlayerInventory playerInventory;

    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, collectRadius);
        foreach (var hit in hits)
        {
            ResourcePickup pickup = hit.GetComponent<ResourcePickup>();
            if (pickup != null)
            {
                pickup.EnableMagnetDelayed(magnetDelay);

                if (pickup.CanBeCollected())
                {
                    float distance = Vector3.Distance(pickup.transform.position, transform.position);
                    float speed = Mathf.Lerp(basePullSpeed, maxPullSpeed, 1f - Mathf.Clamp01(distance / collectRadius));

                    pickup.transform.position = Vector3.MoveTowards(
                        pickup.transform.position,
                        transform.position,
                        speed * Time.deltaTime
                    );

                    if (distance < 0.3f)
                    {
                        playerInventory.AddResource(pickup.resourceData); // теперь передаём ResourceData
                        Destroy(pickup.gameObject);
                    }
                }
            }
        }
    }
}
