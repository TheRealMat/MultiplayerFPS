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

    [ServerRpc(RequireOwnership = false)]
    void SpawnPlayerServerRpc(ServerRpcParams serverRpcParams = default)
    {
        if (NetworkManager.Singleton.ConnectedClients[serverRpcParams.Receive.SenderClientId].PlayerObject != null) return;

        NetworkObject player = Instantiate(playerPrefab);

        player.SpawnAsPlayerObject(serverRpcParams.Receive.SenderClientId);
    }
}
