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
    private GameObject Player2;

    private void Initalize()
    {
        Player = Resources.Load<GameObject>("Prefabs/Player");
        Player2 = Resources.Load<GameObject>("Prefabs/Player 2");
    }

    public void InvokePlayer()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Instance.PlayerId = 0;
    }

    public void InvokePlayer2()
    {
        SceneManager.LoadScene("MainScene");
        GameManager.Instance.PlayerId = 1;
    }

}



