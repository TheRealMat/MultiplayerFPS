using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EscMenu : MonoBehaviour
{
    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
