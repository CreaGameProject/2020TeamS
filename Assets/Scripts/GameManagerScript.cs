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
        if(PlayerPrefs.GetInt("PLAYED") == 0){
            volumeBGM = 0.2f;
            volumeSE = 0.2f;
            SaveVolumePlayerPrefs();
            PlayerPrefs.SetInt("PLAYED",1);
        }
        audioSource = GetComponent<AudioSource>();

        RoadVolumePlayerPrefs();
        //audioSource.volume = volumeBGM;

        DontDestroyOnLoad(this.gameObject);
        Invoke("GoToTitle",6.0f);



        
    }

    


    private void Update(){
        audioSource.volume = volumeBGM;
    }

    private void GoToTitle(){
        SceneManager.LoadScene("Title");
    }


    public void RoadVolumePlayerPrefs(){
        volumeBGM = PlayerPrefs.GetFloat("BGM");
        volumeSE =  PlayerPrefs.GetFloat("SE");
    }

    public void SaveVolumePlayerPrefs(){
        PlayerPrefs.SetFloat("BGM",volumeBGM);
        PlayerPrefs.SetFloat("SE",volumeSE);
    }



}
