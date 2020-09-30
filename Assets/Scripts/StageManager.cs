﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] GameObject plane;

    public int length;
    public int width;
    public float distance;

    GameObject panel;

    [System.NonSerialized] public List<PanelScript> panelScripts;


    private float gameTimer = 90.0f;
    [SerializeField] private Text timerText;

    [System.NonSerialized] public bool isCombo;
    [System.NonSerialized] public int comboTimes = 0;
    private float comboTimer = 5.0f;
    [System.NonSerialized] public int score = 0;
    [SerializeField] private Text scoreText;

    [System.NonSerialized] public bool isGame = false;
    private float countDownTimer = 3.0f;
    private bool isCountDown = true;
    [SerializeField] private GameObject countDownBG;
    [SerializeField] private Text countDownText;
    private bool isWhistle = true;

    [SerializeField] private AudioClip BGM;
    [SerializeField] private AudioClip countDownSE;
    [SerializeField] private AudioClip whistleSE;

    public enum PlayerColor
    {
        YELLOW,
        RED,
        BLUE
    }

    public PlayerColor playerColor;
    private int playerColorNum;

    [SerializeField] private GameObject playerYellow;
    [SerializeField] private GameObject playerRed;
    [SerializeField] private GameObject playerBlue;
    [SerializeField] private Sprite[] playerSprites;
    [SerializeField] private Image charaImage;
    [SerializeField] private Image subColorTimerBG;
    [SerializeField] private Image subColorCountDownBG;
    [SerializeField] private Image cloud;
    private Color playerColorMain;
    private Color playerColorSub;

    void Start()
    {

        SetStageColor();

        StartCoroutine("StageCreate");
        panelScripts = new List<PanelScript>();

        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(countDownSE);


    }

    
    void Update()
    {
        if (isCountDown)
        {
            countDownTimer -= Time.deltaTime;
            countDownText.text = countDownTimer.ToString("f0");

            if(countDownTimer < 0.5f) {
                countDownText.text = "GO";
            }

            if(countDownTimer <= 0)
            {
                

                isCountDown = false;
                isGame = true;
                audioSource.PlayOneShot(BGM);
                countDownBG.SetActive(false);
            }
        }

        timerText.text = gameTimer.ToString("f0");
        scoreText.text = "SCORE:" + score.ToString();

        if(gameTimer <= 0) {
            isGame = false;
            if (isWhistle)
            {
                audioSource.PlayOneShot(whistleSE);
                isWhistle = false;
            }
        }
        
        if(isGame)
        {
            gameTimer -= Time.deltaTime;
        }


        if (isCombo)
        {
            comboTimer -= Time.deltaTime;
            if(comboTimer <= 0.0f)
            {
                isCombo = false;
            }
        }
    }


    private void SetStageColor()
    {

        switch (playerColor)
        {
            case PlayerColor.YELLOW:
                playerColorMain = new Color32(255, 206, 140, 255);
                playerColorSub = new Color32(228, 149, 148, 255);
                playerYellow.SetActive(true);
                charaImage.sprite = playerSprites[0];
                break;

            case PlayerColor.RED:
                playerColorMain = new Color32(231, 130, 156, 255);
                playerColorSub = new Color32(146, 111, 183, 255);
                playerRed.SetActive(true);
                charaImage.sprite = playerSprites[1];
                break;

            case PlayerColor.BLUE:
                playerColorMain = new Color32(93, 134, 203, 255);
                playerColorSub = new Color32(107, 198, 194, 255);
                playerBlue.SetActive(true);
                charaImage.sprite = playerSprites[2];
                break;
        }

        subColorTimerBG.color = playerColorSub;
        subColorCountDownBG.color = playerColorSub;
        cloud.color = playerColorMain;
    }

    public void AllPanelChangeColor()
    {
        foreach(PanelScript panelScript in panelScripts)
        {
            if(panelScript != null)
            {
                panelScript.ChangeTexture();
            }
        }
    }

    IEnumerator StageCreate()
    {
        for (int dz = 0; dz < length; dz++)
        {
            for (int dx = 0; dx < width; dx++)
            {

                if (dz % 2 == 0)
                {
                    panel = Instantiate(plane, new Vector3(dx * distance, 0, dz * distance * Mathf.Sin(Mathf.PI / 3)), transform.rotation);
                }
                else
                {
                    panel = Instantiate(plane, new Vector3(dx * distance + distance / 2, 0, dz * distance * Mathf.Sin(Mathf.PI / 3)), transform.rotation);
                }

                yield return new WaitForSeconds(1 / 80);

                panelScripts.Add(panel.GetComponent<PanelScript>());

                yield return new WaitForSeconds(1 / 80);
            }
        }
    }


  

    public IEnumerator CreateNewPanel()
    {
        for (int dx = 0; dx < width; dx++)
        {
            if (length % 2 == 0)
            {
                panel = Instantiate(plane, new Vector3(dx * distance, 0, length * distance * Mathf.Sin(Mathf.PI / 3)), transform.rotation);
            }
            else
            {
                panel = Instantiate(plane, new Vector3(dx * distance + distance / 2, 0, length * distance * Mathf.Sin(Mathf.PI / 3)), transform.rotation);
            }

            yield return new WaitForSeconds(1 / 80);

            panelScripts.Add(panel.GetComponent<PanelScript>());

            yield return new WaitForSeconds(1 / 80);

            //panelScripts.RemoveAt(dx);
        }
        length++;
    }

   
    public void AddScore(int addPoint,bool combo)
    {

        if (combo && isCombo)
        {
            comboTimes++;
            isCombo = true;
        }

        if (combo)
        {
            isCombo = true;
            comboTimer = 5.0f;
        }
        else
        {
            comboTimes = 0;
            isCombo = false;
        }


        score += (5 * addPoint + comboTimes) * 20;

        
    }

}
