using Mirror;
using UnityEngine;


[RequireComponent(typeof(NetworkIdentity))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    private float minSpeed = 1f;
    
    [SerializeField]
    private float maxSpeed = 5f;
    
    [SerializeField]
    private float timeSinceLastDirectionChange = 0f;
    
    [SerializeField]
    private float directionChangeInterval = 2f;
    
    private Vector3 moveDirection;
    
    private NetworkIdentity networkIdentity;
    
    private float currentSpeed = 0f;
    
    private void Start()
    {
        moveDirection = GetRandomDirection();
    }
    
    private Vector3 GetRandomDirection()
    {
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        
        return new Vector3(randomX, 0, randomZ);
    }
    
    private float GetRandomSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }
    
    private void FixedUpdate()
    {
        timeSinceLastDirectionChange += Time.fixedDeltaTime;
        
        if (timeSinceLastDirectionChange >= directionChangeInterval)
        {
            moveDirection = GetRandomDirection();
            currentSpeed = GetRandomSpeed();
            timeSinceLastDirectionChange = 0f;
        }
        
        transform.Translate(moveDirection * (currentSpeed * Time.fixedDeltaTime));
    }
}
