using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] NetworkObject playerPrefab;
    public override void OnNetworkSpawn()
    {
        if (!IsClient) return;
        SpawnPlayerServerRpc();
    }

    // client tells server it would like to spawn
    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ServerRpcParams serverRpcParams = default)
    {
        // client is already spawned
        if (NetworkManager.Singleton.ConnectedClients[serverRpcParams.Receive.SenderClientId].PlayerObject != null) return;

        SpawnPlayer(serverRpcParams.Receive.SenderClientId);
    }

    // called by server
    public void SpawnPlayer(ulong clientId)
    {
        // destroy player for respawning if already exists
        if(NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject != null)
        {
            NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.Despawn();
        }

        NetworkObject player = Instantiate(playerPrefab);

        player.SpawnAsPlayerObject(clientId);
    }
}
