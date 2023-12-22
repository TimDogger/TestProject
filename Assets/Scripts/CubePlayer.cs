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

    [SerializeField]
    private CubePlayerUI cubePlayerUI;
    
    private CubePlayerUI CubePlayerUI
    {
        get
        {
            if (!cubePlayerUI)
            {
                cubePlayerUI = FindObjectOfType<CubePlayerUI>();
            }

            return cubePlayerUI;
        }
    }

    private void Start()
    {
        if (networkIdentity.isLocalPlayer)
        {
            meshRenderer.enabled = false;
            CubePlayerUI.InitializePlayer(this);
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
