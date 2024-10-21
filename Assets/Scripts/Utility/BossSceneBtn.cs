using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneBtn : MonoBehaviour
{
    public void ToBoss()
    {
        StartCoroutine(LoadToBoss());
    }

    public IEnumerator LoadToBoss()
    {
        DestroyAllBullets();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BossSceneTest");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        SceneManager.LoadScene("BossSceneTest");
    }

    private void DestroyAllBullets()
    {
        var allProjectiles = FindObjectsOfType<ProjectileController>();
        foreach (var projectile in allProjectiles)
        {
            projectile.gameObject.SetActive(false);
        }
    }
}
