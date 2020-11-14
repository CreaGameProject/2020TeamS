using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    [System.NonSerialized] public AudioSource audioSource;
    [System.NonSerialized] public int playerNumber = 0;

    public float volumeBGM { get; private set; }
    public void SetVolumeBGM(float volume) => volumeBGM = volume;
    public float volumeSE { get; private set; }
    public void SetVolumeSE(float volume) => volumeSE = volume;

    private void Start()
    {
        volumeBGM = 0.2f;
        volumeSE = 0.2f;
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = volumeBGM;

        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Title");
    }


}
