using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnClick : MonoBehaviour
{
    public GameObject loadingimage;
    
    public void LoadScene(int level)
    {
        loadingimage.SetActive(true);
        StartCoroutine(LoadingDelay(level));
        //SceneManager.LoadScene(level);
        //The below method has deprecated.
        //Application.LoadLevel(level);
    }

    IEnumerator LoadingDelay(int level)
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }
}
