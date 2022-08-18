using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : NetworkBehaviour
{
    [SerializeField]
    float sensitivity = 100f;

    Vector2 rotation;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            Destroy(this.gameObject);
        } 
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        rotation.y += mouseX;
        rotation.x -= mouseY;
        rotation.x = Mathf.Clamp(rotation.x, -90, 90);

        transform.localRotation = Quaternion.Euler(rotation.x, 0 , 0);
        transform.parent.transform.eulerAngles = new Vector2(0, rotation.y);
    }
}
