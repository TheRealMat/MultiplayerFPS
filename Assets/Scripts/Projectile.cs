using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //[SerializeField] float dropoff = -9.81f;
    [SerializeField] float projectilespeed = 10;
    [SerializeField] float projectileLifetime = 5;


    // do SphereCastAll for explosions


    private void Start()
    {
        Destroy(gameObject, projectileLifetime);
    }
    private void Update()
    {
        transform.position += transform.forward * projectilespeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // fix this later
        if(other.gameObject.tag == "Player")
        {
            // use the third overload to turn off the ignorecollision after half a second or so, maybe check OnTriggerExit
            Physics.IgnoreCollision(other, GetComponent<Collider>());

            // needs a way to prevent shotgun projectiles from colliding with eachother

            return;
        }

        Destroy(gameObject);
    }
}
