using UnityEngine;

public class PlayerMagnet : MonoBehaviour
{
    public float radius = 3f;
    public float magnetDelay = 0.3f;

    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit in hits)
        {
            ResourcePickup pickup = hit.GetComponent<ResourcePickup>();
            if (pickup != null && !pickup.canBeCollected)
            {
                StartCoroutine(StartMagnetWithDelay(pickup));
            }
        }
    }

    System.Collections.IEnumerator StartMagnetWithDelay(ResourcePickup pickup)
    {
        yield return new WaitForSeconds(magnetDelay);
        pickup.StartMagnet(transform);
    }
}
