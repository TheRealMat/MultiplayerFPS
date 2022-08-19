using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponBehavior : MonoBehaviour
{

    enum FireType
    {
        single,
        burst,
        auto,
        none
    };
    [SerializeField] FireType firetype;

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
                    fire();
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
                        fire();
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
                        fire();
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
                    fire();
                }
                break;
        }
    }
    void fire()
    {
        for (int i = 0; i < roundsPerShot; i++)
        {
            switch(projectileType){
                case ProjectileType.none:
                    break;
                case ProjectileType.projectile:
                    GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                    projectile.transform.Rotate(Spread());
                    break;
                case ProjectileType.hitscan:
                    // TODO
                    break;
            }
        }
    }

    Vector3 Spread()
    {
        return transform.forward + new Vector3(Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread), Random.Range(-bulletSpread, bulletSpread));
    }
}
