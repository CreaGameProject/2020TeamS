using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManagerScript : MonoBehaviour
{
    [SerializeField] private AudioClip titleBGM;
    private GameManagerScript gameManagerScript;

    [SerializeField] private GameObject settingBackGround;
    [SerializeField] private GameObject volumeSetting;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject howToPlayButton;
    [SerializeField] private GameObject settingButton;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Text bgmText;
    [SerializeField] private Slider seSlider;
    [SerializeField] private Text seText;

    private void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManagerScript>();
            bgmSlider.value = gameManagerScript.volumeBGM;
            seSlider.value = gameManagerScript.volumeSE;

            gameManagerScript.audioSource.clip = titleBGM;
            gameManagerScript.audioSource.Play();
        }
    }

    public void OnChangeBGMSlider(){
        gameManagerScript.SetVolumeBGM(bgmSlider.value);
        bgmText.text = string.Format("{0:0}", bgmSlider.value * 100);
    }

    public void OnChangeSESlider(){
        gameManagerScript.SetVolumeSE(seSlider.value);
        seText.text = string.Format("{0:0}", seSlider.value * 100);
    }

    public void OnSettingButtonClicked(){
        settingBackGround.SetActive(true);
        volumeSetting.SetActive(true);
        playButton.SetActive(false);
        howToPlayButton.SetActive(false);
        settingButton.SetActive(false);
    }

    public void OnVolumeEnterButtonClicked(){
        settingBackGround.SetActive(false);
        volumeSetting.SetActive(false);
        playButton.SetActive(true);
        howToPlayButton.SetActive(true);
        settingButton.SetActive(true);

        gameManagerScript.SaveVolumePlayerPrefs();
    }

    
}
