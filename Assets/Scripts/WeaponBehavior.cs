using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UIElements;

public class WeaponBehavior : NetworkBehaviour
{

    enum FireType
    {
        single,
        auto,
    };
    [SerializeField] FireType firetype;

    enum ProjectileType
    {
        projectile,
        hitscan,
        none
    }
    [SerializeField] ProjectileType projectileType;

    // how long between each projectile in burst/auto
    [SerializeField] float fireRate = 10f;

    [SerializeField] float bulletSpread = 0.5f;

    [SerializeField] int roundsPerShot = 1;

    [SerializeField] float hitscanRange;

    [SerializeField] GameObject projectilePrefab;

    float lastFired;
    bool canFire;

    private void Start()
    {
        lastFired = -fireRate;
    }

    public void Fire()
    {
        if (!IsOwner) return;

        if (firetype == FireType.single)
        {
            if (Time.time - lastFired >= fireRate)
            {
                lastFired = Time.time;
                canFire = true;
            }
            else
            {
                canFire = false;
            }
        }
        else if (firetype == FireType.auto)
        {
            if (Time.time - lastFired >= fireRate)
            {
                lastFired = Time.time;
                canFire= true;
            }
            else
            {
                canFire= false;
            }
        }

        if (canFire != true) return;
        for (int i = 0; i < roundsPerShot; i++)
        {
            if (projectileType == ProjectileType.projectile)
            {
                FireServerRpc(transform.position, transform.rotation);
                // fake projectile?
            }
            else if (projectileType == ProjectileType.none) return;
            else if (projectileType == ProjectileType.hitscan) return; // TODO
        }
    }
    [ServerRpc]
    void FireServerRpc(Vector3 position, Quaternion rotation, ServerRpcParams serverRpcParams = default)
    {
        // fire the projectile here
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);

        projectile.transform.Rotate(Spread());
        projectile.GetComponent<Projectile>().projectileOwner = serverRpcParams.Receive.SenderClientId;
        projectile.GetComponent<NetworkObject>().Spawn();
        FireClientRpc(position, rotation);
    }
    [ClientRpc]
    void FireClientRpc(Vector3 position, Quaternion rotation)
    {
        //if (IsOwner) return;
        // fake projectile here?
    }

    Vector3 Spread()
    {
        return transform.forward + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
    }
}
