using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponBehavior behavior1;
    [SerializeField] WeaponBehavior behavior2;

    public void Fire1(bool fireToggle)
    {
        if (behavior1 == null) return;
        FireWeapon(behavior1, fireToggle);
    }
    public void Fire2(bool fireToggle)
    {
        if (behavior2 == null) return;
        FireWeapon(behavior2, fireToggle);
    }

    public void FireWeapon(WeaponBehavior weaponBehavior, bool fireToggle)
    {
        weaponBehavior.Fire(fireToggle);
    }
}
