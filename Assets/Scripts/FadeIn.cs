using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
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
}
