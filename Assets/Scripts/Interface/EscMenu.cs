using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class EscMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel;
    public void Disconnect()
    {
        NetworkManager.Singleton.Shutdown();
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
