using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{

    public AudioClip mainTheme;
    public AudioClip menuTheme;

    void Start()
    {
        AudioManager.instance.PlayMusic(menuTheme, 2);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 4)
        {
            AudioManager.instance.PlayMusic(mainTheme, 0);
        }
    }
}