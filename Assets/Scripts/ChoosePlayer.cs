using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePlayer : MonoBehaviour
{
    public GameObject fadeInScreen;

    void Start()
    {
        Invoke("disableFadeIn", 1.1f);
    }

    void disableFadeIn()
    {
        fadeInScreen.SetActive(false);
    }
    public void bPlayer()
    {
        Spawner.characterSelected = 0;
        PlayerPrefs.SetInt("selectCharacter", Spawner.characterSelected);
        Invoke("loadBScene", 0.7f);
    }

    public void tPlayer()
    {
        Spawner.characterSelected = 1;
        PlayerPrefs.SetInt("selectCharacter", Spawner.characterSelected);
        Invoke("loadTScene", 0.7f);
    }
    void loadBScene()
    {
        SceneManager.LoadScene(2);
    }

    void loadTScene()
    {
        SceneManager.LoadScene(4);
    }


}
