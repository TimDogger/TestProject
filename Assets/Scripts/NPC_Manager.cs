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
        Instance = this;
    }
    
    public void RequestSpawnNPC(Vector3 position, Quaternion rotation)
    {
        if (isServer)
        {
            SpawnNpc(position, rotation);
        }
        else
        {
            CmdSpawnNPC(position, rotation);
        }
    }
    
    [Command]
    private void CmdSpawnNPC(Vector3 position, Quaternion rotation)
    {
        SpawnNpc(position, rotation);
    }
    
    private void SpawnNpc(Vector3 position, Quaternion rotation)
    {
        NPC npc = Instantiate(npcPrefab, position, rotation);
        npcs.Add(npc);
    }
    
    public void RequestRemoveAllNPCs()
    {
        if (isServer)
        {
            RemoveAllNPCs();
        }
        else
        {
            CmdRemoveAllNPCs();
        }
    }
    
    [Command]
    private void CmdRemoveAllNPCs()
    {
        RemoveAllNPCs();
    }
    
    private void RemoveAllNPCs()
    {
        foreach (NPC npc in npcs)
        {
            Destroy(npc.gameObject);
        }
        
        npcs.Clear();
    }
}
