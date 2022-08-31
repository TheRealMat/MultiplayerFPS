using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Projectile : NetworkBehaviour
{
    [SerializeField] int damage = 30;
    [SerializeField] float launchSpeed = 500;
    [SerializeField] float projectileLifetime = 5;

    [HideInInspector] public ulong projectileOwner;

    // do SphereCastAll for explosions

    private void Start()
    {
        if (!IsServer) return;
        GetComponent<Rigidbody>().AddForce(transform.forward * launchSpeed);
        Destroy(gameObject, projectileLifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;

        if(other.gameObject.tag == "Player")
        {
            if (projectileOwner == other.gameObject.GetComponent<NetworkObject>().OwnerClientId)
            {
                // should use the third overload to turn off the ignorecollision after half a second or so, maybe check OnTriggerExit
                Physics.IgnoreCollision(other, GetComponent<Collider>());
                return;
            }
            other.GetComponent<Health>().TakeDamage(damage);
        }
        else if (other.gameObject.tag == "Projectile")
        {
            // perhaps some projectiles should be able to collide?
            if(other.GetComponent<Projectile>().projectileOwner == this.projectileOwner)
            {
                return;
            }
        }

        Destroy(gameObject);
    }
}
