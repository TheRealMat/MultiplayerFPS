using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapons : MonoBehaviour
{
    InputActionAsset actions;
    [SerializeField] Weapon[] weapons = new Weapon[2];
    Weapon equippedWeapon;
    bool firing1;
    bool firing2;

    void Start()
    {
        actions = GetComponent<PlayerInput>().actions;
        equippedWeapon = weapons[0];
    }
    private void Update()
    {
        if (firing1)
        {
            equippedWeapon.Fire1();
        }
        if (firing2)
        {
            equippedWeapon.Fire2();
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
