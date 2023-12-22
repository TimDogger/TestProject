using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NPC_Manager : NetworkBehaviour
{
    [SerializeField]
    private List<NPC> npcs;
    
    [SerializeField]
    private NPC npcPrefab;
    
    [SerializeField]
    private NetworkIdentity networkIdentity;
    
    public static NPC_Manager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Destroying duplicate.");
            Destroy(gameObject);
        }
    }
    
    public void RequestSpawnNPC(Vector3 position, Quaternion rotation)
    {
        if (isServer)
        {
            SpawnNpcInternal(position, rotation);
        }
        else
        {
            Server_SpawnNPC(position, rotation);
        }
    }
    
    public void RequestRemoveAllNPCs()
    {
        if (isServer)
        {
            RemoveAllNPCsInternal();
        }
        else
        {
            Server_RemoveAllNPCs();
        }
    }
    
    [Command]
    private void Server_SpawnNPC(Vector3 position, Quaternion rotation)
    {
        SpawnNpcInternal(position, rotation);
    }
    
    [Command]
    private void Server_RemoveAllNPCs()
    {
        RemoveAllNPCsInternal();
    }
    
    private void SpawnNpcInternal(Vector3 position, Quaternion rotation)
    {
        NPC npc = Instantiate(npcPrefab, position, rotation);
        NetworkServer.Spawn(npc.gameObject);
        npcs.Add(npc);
    }
    
    private void RemoveAllNPCsInternal()
    {
        foreach (NPC npc in npcs)
        {
            NetworkServer.Destroy(npc.gameObject);
        }
        
        npcs.Clear();
    }
}
