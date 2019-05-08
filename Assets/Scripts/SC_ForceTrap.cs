using UnityEngine;
using System.Collections;

public class SC_ForceTrap : MonoBehaviour
{
    [SerializeField]
    float radius = 2f;

    [SerializeField]
    float force = 10f;

    [SerializeField]
    LayerMask layerMask;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player") || other.gameObject.name.Contains("Agent"))
        {
            StartCoroutine(Explosion());
        }
    }

   
    private IEnumerator Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(force, transform.position, radius, 0, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
