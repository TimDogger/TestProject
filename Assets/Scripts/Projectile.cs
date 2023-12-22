using Mirror;
using UnityEngine;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    
    [SerializeField]
    private float timeToLive = 5f;
    
    private float livingTime = 0f;
    
    private bool simulate = true;
    
    private Rigidbody rigidbodyComponent;
    private Collider colliderComponent;
    
    private void Awake()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        colliderComponent = GetComponent<Collider>();
        livingTime = 0f;
    }

    private void FixedUpdate()
    {
        if (!simulate) return;
        
        rigidbodyComponent.MovePosition(transform.position + transform.up * (speed * Time.fixedDeltaTime));
        livingTime += Time.fixedDeltaTime;
        if (livingTime >= timeToLive)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damageable"))
        {
            AttachToHitObject(other.gameObject);
            return;
        }

        if (other.CompareTag("Landscape"))
        {
            NetworkServer.Destroy(gameObject);
        }
    }
    
    private void AttachToHitObject(GameObject hitObject)
    {
        Debug.Log($"AttachToHitObject {hitObject.name}");
        simulate = false;
        transform.SetParent(hitObject.transform);
        rigidbodyComponent.isKinematic = true;
        colliderComponent.enabled = false;
    }
}
