using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectManager1 : MonoBehaviour
{
    private void Start()
    {
        Initalize();
    }

    private GameObject Player;

    private void Initalize()
    {
        Player = Resources.Load<GameObject>("Prefabs/Player");
    }

    public void InvokePlayer()
    {
        Instantiate(Player);
        SceneManager.LoadScene("MainScene");
    }
    
}



