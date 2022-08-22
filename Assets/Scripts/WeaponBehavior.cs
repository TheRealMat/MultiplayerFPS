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
        burst,
        auto,
        none
    };
    [SerializeField] FireType firetype;

    public override void OnNetworkSpawn()
    {
        //if (!IsOwner) Destroy(this);
    }

    enum ProjectileType
    {
        projectile,
        hitscan,
        none
    }
    [SerializeField] ProjectileType projectileType;

    // how many times shot for each burst
    [SerializeField] int burstAmount;

    // how long between each projectile in burst/auto
    [SerializeField] float fireRate = 10f;

    //how long between each burst/single
    [SerializeField] float fireDelay = 0.1f;

    [SerializeField] float bulletSpread = 0.5f;

    [SerializeField] int roundsPerShot = 1;

    // only relevant for hitscan
    [SerializeField] float range;

    [SerializeField] GameObject projectilePrefab;

    float lastFired;
    int shotsFired;

    private void Start()
    {
        lastFired = -fireDelay -fireRate;
    }

    // this should be handled by events
    private void Update()
    {
        if (!IsOwner) return;
        switch (firetype)
        {
            case FireType.none:
                break;
            case FireType.single:
                if (!Input.GetMouseButtonDown(0))
                {
                    return;
                }
                if (Time.time - lastFired >= fireDelay)
                {
                    lastFired = Time.time;
                    Fire();
                }
                break;
            case FireType.burst:
                // burst start
                if (Input.GetMouseButtonDown(0))
                {
                    if (Time.time - lastFired >= fireDelay)
                    {
                        lastFired = Time.time;
                        shotsFired++;
                        Fire();
                        return;
                    }
                }
                // continue burst
                if (shotsFired > 0 && shotsFired <= burstAmount)
                {
                    if (Time.time - lastFired >= fireRate)
                    {
                        shotsFired++;
                        lastFired = Time.time;
                        Fire();
                        if (shotsFired >= burstAmount)
                        {
                            shotsFired = 0;
                        }
                    }
                }
                break;
            case FireType.auto:
                if (!Input.GetMouseButton(0))
                {
                    return;
                }
                if (Time.time - lastFired >= fireRate)
                {
                    lastFired = Time.time;
                    Fire();
                }
                break;
        }
    }
    void Fire()
    {
        for (int i = 0; i < roundsPerShot; i++)
        {
            switch(projectileType){
                case ProjectileType.none:
                    break;
                case ProjectileType.projectile:
                    FireServerRpc(transform.position, transform.rotation);
                    // fake projectile?
                    break;
                case ProjectileType.hitscan:
                    // TODO
                    break;
            }
        }
    }
    [ServerRpc]
    void FireServerRpc(Vector3 position, Quaternion rotation)
    {
        // fire the projectile here
        GameObject projectile = Instantiate(projectilePrefab, position, rotation);
        projectile.transform.Rotate(Spread());
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
