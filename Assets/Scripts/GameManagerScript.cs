﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    [System.NonSerialized] public AudioSource audioSource;


    [System.NonSerialized] public int playerNumber = 0;
    


    private void Start()
    {

        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Title");

        
    }


}
