using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    [System.NonSerialized] public AudioSource audioSource;


    [System.NonSerialized] public int playerNumber = 0;

    public float volumeBGM { get; private set; }
    public float voluemSE { get; private set; }


    private void Start()
    {
        volumeBGM = 0.2f;
        voluemSE = 0.2f;
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = volumeBGM;

        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Title");
    }


}
