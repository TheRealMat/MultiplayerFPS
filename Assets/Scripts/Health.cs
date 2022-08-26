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
    }

    void OnValueChanged(int previousHealth, int currentHealth)
    {
        if (!IsOwner) return;
        Debug.Log($"i had {previousHealth} health, now i have {currentHealth}");
    }
}
