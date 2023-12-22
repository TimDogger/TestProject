using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubePlayer : NetworkBehaviour
{
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

    public Camera PlayerCamera => mainCamera;
    
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private NetworkIdentity networkIdentity;
    
    [SerializeField]
    private Projectile projectilePrefab;
    
    [SerializeField]
    private Camera mainCamera;

    private CubePlayerUI cubePlayerUI;
    

    private void Start()
    {
        if (networkIdentity.isLocalPlayer)
        {
            meshRenderer.enabled = false;
            mainCamera.gameObject.tag = "MainCamera";
            CubePlayerUI.InitializePlayer(this);
        }
        else
        {
            mainCamera.enabled = false;
        }
    }

    public void OnShootPerfomed(InputAction.CallbackContext context)
    {
        if (networkIdentity.isLocalPlayer && context.performed)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (!networkIdentity.isLocalPlayer) return;
        
        Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * .3f;
        Quaternion rotation = Camera.main.transform.rotation * Quaternion.Euler(90, 0, 0);
            
        if (isServer)
        {
            SpawnArrowInternal(position, rotation);
        }
        else
        {
            Server_SpawnArrow(position, rotation);
        }
    }
    
    public void OnSpawnNPCDown(InputAction.CallbackContext context)
    {
        if (networkIdentity.isLocalPlayer && context.performed)
        {
            SpawnNPC();
        }
    }

    public void SpawnNPC()
    {
        Vector3 position = transform.position + transform.forward * 2f;
        Quaternion rotation = transform.rotation;
        
        NPC_Manager.Instance.RequestSpawnNPC(position, rotation);
    }
    
    [Command]
    private void Server_SpawnArrow(Vector3 position, Quaternion rotation)
    {
        SpawnArrowInternal(position, rotation);
    }
    
    private void SpawnArrowInternal(Vector3 position, Quaternion rotation)
    {
        var projectileGO = Instantiate(projectilePrefab.gameObject, position, rotation); 
        NetworkServer.Spawn(projectileGO);
    }
}
