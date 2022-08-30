using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] int maxHealth = 100;
    NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        currentHealth.OnValueChanged += OnValueChanged;

        if (!IsServer) return;
        currentHealth.Value = maxHealth;
    }

    public override void OnNetworkDespawn()
    {
        currentHealth.OnValueChanged -= OnValueChanged;
    }

    // called by server
    public void TakeDamage(int damage)
    {
        if (!IsServer) return;
        currentHealth.Value -= damage;

        if (currentHealth.Value <= 0)
        {
            // there should be a death screen first
            // this should be handled by an event or something
            FindObjectOfType<SpawnManager>().SpawnPlayer(this.OwnerClientId);
        }
    }

    void OnValueChanged(int previousHealth, int currentHealth)
    {
        if (!IsOwner) return;
        Debug.Log($"i had {previousHealth} health, now i have {currentHealth}");
    }
}
