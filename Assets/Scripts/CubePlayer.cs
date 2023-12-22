using System;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubePlayer : NetworkBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private NetworkIdentity networkIdentity;

    private void Start()
    {
        if (networkIdentity.isLocalPlayer)
        {
            meshRenderer.enabled = false;
        }
    }

    public void ShootArrow()
    {
        
    }
    
    public void SpawnNpc(InputAction.CallbackContext context)
    {
        if (networkIdentity.isLocalPlayer && context.performed)
        {
            Vector3 position = transform.position + transform.forward * 2f;
            Quaternion rotation = transform.rotation;
            
            NPC_Manager.Instance.RequestSpawnNPC(position, rotation);
        }
    }
}
