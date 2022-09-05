using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Weapon : MonoBehaviour
{
    [SerializeField] WeaponBehavior behavior1;
    [SerializeField] WeaponBehavior behavior2;

    bool firedBefore1 = false;
    bool firedBefore2 = false;

    public void Fire1()
    {
        if (behavior1 == null) return;
        FireWeapon(behavior1, firedBefore1);
        firedBefore1 = true;
    }
    public void Fire2()
    {
        if (behavior2 == null) return;
        FireWeapon(behavior2, firedBefore2);
        firedBefore2 = true;
    }

    public void StopFire1()
    {
        firedBefore1 = false;
    }
    public void StopFire2()
    {
        firedBefore2 = false;
    }

    public void FireWeapon(WeaponBehavior weaponBehavior, bool fireToggle)
    {
        weaponBehavior.Fire(fireToggle);
    }
}
