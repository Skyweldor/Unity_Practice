using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseClickExample : MonoBehaviour
{
    public GameObject loadingImage;
    public int menuLevel = 0;

    private void OnMouseDown()
    {
    
        loadingImage.SetActive(true);
        StartCoroutine(LoadingDelay());
        //SceneManager.LoadScene(menuLevel);

    }

    IEnumerator LoadingDelay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(menuLevel);
    }
}
