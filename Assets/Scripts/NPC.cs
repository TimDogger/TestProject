using Mirror;
using UnityEngine;


[RequireComponent(typeof(NetworkIdentity))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    
    [SerializeField]
    private float timeSinceLastDirectionChange = 0f;
    
    [SerializeField]
    private float directionChangeInterval = 2f;
    
    private Vector3 moveDirection;
    
    private NetworkIdentity networkIdentity;
    
    private void Start()
    {
        moveDirection = GetRandomDirection();
    }
    
    Vector3 GetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        
        return new Vector3(randomX, 0, randomZ);
    }
    
    private void FixedUpdate()
    {
        timeSinceLastDirectionChange += Time.fixedDeltaTime;
        
        if (timeSinceLastDirectionChange >= directionChangeInterval)
        {
            moveDirection = GetRandomDirection();
            timeSinceLastDirectionChange = 0f;
        }
        
        transform.Translate(moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }
}
