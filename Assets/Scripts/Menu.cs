using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Animator anim;
    public GameObject SplashScreen;


    void Start()
    {
        Invoke("EraseSplash", 3.6f);
    }
    public void PlayGame()
    {
        anim.SetBool("fade", true);
        Invoke("loadScene", 0.7f);
    }

    void loadScene()
    {
        SceneManager.LoadScene(1);
    }

    void EraseSplash()
    {
        SplashScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
