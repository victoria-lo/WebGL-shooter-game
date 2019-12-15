using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public string[] sentences;
    public Text textDisplay;
    private int index;
    public float typingSpeed;
    public GameObject dialogBox;

    public GameObject continueButton;

    private AudioSource sound;

    public Animator anim;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        Invoke("startType", 2.0f);

    }

    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    void startType()
    {
        dialogBox.SetActive(true);
        StartCoroutine(Type());
    }

    public void NextSentence()
    {
        sound.Play();
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else if (index == sentences.Length - 1)
        {
            dialogBox.SetActive(false);
            anim.SetBool("fade", true);
            Invoke("loadScene", 1.1f);
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

    public void loadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
