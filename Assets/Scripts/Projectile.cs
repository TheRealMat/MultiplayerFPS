using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Projectile : NetworkBehaviour
{
    [SerializeField] float dropoff = 0;
    [SerializeField] float projectilespeed = 10;
    [SerializeField] float projectileLifetime = 5;

    [HideInInspector] public ulong projectileOwner;

    // do SphereCastAll for explosions

    Vector3 projectileVelocity;

    private void Start()
    {
        if (!IsServer) return;
        Destroy(gameObject, projectileLifetime);
    }
    private void Update()
    {
        transform.position += transform.forward * projectilespeed * Time.deltaTime;
        projectileVelocity.y += dropoff * Time.deltaTime;
        transform.position += Vector3.down * projectileVelocity.y * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;
        // fix this later
        if(other.gameObject.tag == "Player")
        {
            // use the third overload to turn off the ignorecollision after half a second or so, maybe check OnTriggerExit
            Physics.IgnoreCollision(other, GetComponent<Collider>());

            return;
        }
        if (other.gameObject.tag == "Projectile")
        {
            // perhaps some projectiles should be able to collide
            if(other.GetComponent<Projectile>().projectileOwner == this.projectileOwner)
            {
                return;
            }
        }

        Destroy(gameObject);
    }
}
