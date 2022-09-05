using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{
    [SerializeField] Weapon[] weapons = new Weapon[2];
    Weapon equippedWeapon;
    bool firing1;
    bool firing2;

    void Start()
    {
        SwitchWeapon(0);
    }
    void SwitchWeapon(int weaponIndex)
    {
        if(equippedWeapon != null)
        {
            equippedWeapon.gameObject.SetActive(false);
        }
        equippedWeapon = weapons[weaponIndex];
        equippedWeapon.gameObject.SetActive(true);
    }
    public void SwitchWeapon(InputAction.CallbackContext context)
    {
        int weaponIndex = (int)context.ReadValue<float>() - 1;

        if (!context.performed) return;
        if (weaponIndex < 0) return;
        if (weaponIndex > weapons.Length - 1) return;
        if (weapons[weaponIndex] == null) return;

        SwitchWeapon(weaponIndex);
    }

    private void Update()
    {
        if (firing1)
        {
            equippedWeapon.Fire1();
        }
        else
        {
            equippedWeapon.StopFire1();
        }
        if (firing2)
        {
            equippedWeapon.Fire2();
        }
        else
        {
            equippedWeapon.StopFire2();
        }
    }
    public void Fire1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            firing1 = true;
        }
        else if (context.canceled)
        {
            firing1 = false;
        }

    }
    public void Fire2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            firing2 = true;
        }
        else if (context.canceled)
        {
            firing2 = false;
        }

    }

}
