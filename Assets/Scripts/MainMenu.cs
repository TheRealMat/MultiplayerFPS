using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class MainMenu : MonoBehaviour
{
    UnityTransport unityTransport;
    NetworkManager networkManager;

    string address = "127.0.0.1";
    ushort port = 7777;

    private void Start()
    {
        networkManager = NetworkManager.Singleton;
        unityTransport = NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();
    }

    public void StartClient()
    {
        unityTransport.SetConnectionData(address, port);
        networkManager.StartClient();
    }
    public void StopConnecting()
    {
        networkManager.Shutdown();
    }

    public void StartHost()
    {
        unityTransport.SetConnectionData(address, port);
        networkManager.StartHost();
        networkManager.SceneManager.LoadScene("SampleScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
