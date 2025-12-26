using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHitResources : MonoBehaviour
{
    public float hitRadius = 3f;

    // Метод без параметров, подходит для Unity Events
    public void OnHit()
    {
        HitNearbySources();
    }

    private void HitNearbySources()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, hitRadius);
        foreach (var hit in hits)
        {
            ResourceSource source = hit.GetComponent<ResourceSource>();
            if (source != null)
                source.Hit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
