using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponBehavior behavior1;
    [SerializeField] WeaponBehavior behavior2;

    public void Fire1()
    {
        if (behavior1 == null) return;
        FireWeapon(behavior1);
    }
    public void Fire2()
    {
        if (behavior2 == null) return;
        FireWeapon(behavior2);
    }

    public void FireWeapon(WeaponBehavior weaponBehavior)
    {
        weaponBehavior.Fire();
    }
}
