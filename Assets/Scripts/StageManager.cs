using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [System.NonSerialized] public AudioSource audioSource;

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
    private float displayScore;
    private float displayScoreAnimeSpeedFactor = 15.0f;
    private int scoreBasePoint = 100;
    [SerializeField] private Text scoreText;

    [System.NonSerialized] public bool isGame = false;
    private float countDownTimer = 3.0f;
    private bool isCountDown = true;
    [SerializeField] private GameObject countDownBG;
    [SerializeField] private Text countDownText;
    private bool isWhistle = true;

    [SerializeField] private AudioClip stageBGM;
    [SerializeField] private AudioClip rankingBGM;
    [SerializeField] private AudioClip countDownSE;
    [SerializeField] private AudioClip whistleSE;
    [SerializeField] private AudioClip matchSE;
    [SerializeField] private AudioClip missSE;
    [SerializeField] private AudioClip maxComboSE;

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
    [SerializeField] private Sprite[] timerSprites;
    [SerializeField] private Sprite[] countdownSprites;
    [SerializeField] private Sprite[] cloudSprites;
    [SerializeField] private Image charaImage;
    [SerializeField] private Image timerImage;
    [SerializeField] private Image countdownImage;
    [SerializeField] private Image cloudImage;


    private GameObject gameManager { get; set; }

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {
            playerColorNum =  gameManager.GetComponent<GameManagerScript>().playerNumber;
            gameManager.GetComponent<GameManagerScript>().audioSource.Stop();
        }

        displayScore = score;

        SetStageColor();
        StartCoroutine("StageCreate");
        panelScripts = new List<PanelScript>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = gameManager.GetComponent<GameManagerScript>().volumeSE;
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
                gameManager.GetComponent<AudioSource>().PlayOneShot(stageBGM);
                countDownBG.SetActive(false);
            }
        }

        timerText.text = gameTimer.ToString("f0");

        //現在のスコアと表示用のスコアが異なっていれば、現在のスコアになるまで加算する
        if(displayScore < score){
            displayScore += Time.deltaTime * displayScoreAnimeSpeedFactor * Mathf.Abs(score - displayScore);
        }else if(displayScore > score){
            displayScore -= Time.deltaTime * displayScoreAnimeSpeedFactor * Mathf.Abs(score - displayScore);
        }

        //差の絶対値が1未満の場合は同値とみなす
        if(Mathf.Abs(score - displayScore) < 1.0f){
            displayScore = (float)score;
        }


        scoreText.text = "SCORE:" + displayScore.ToString("f0");
        //Debug.Log(displayScore + ":" + score);

        if(gameTimer <= 0) {
            isGame = false;
            if (isWhistle)
            {
                audioSource.pitch = 1.0f;
                audioSource.PlayOneShot(whistleSE);
                isWhistle = false;

                Invoke("Ranking", 2.0f);
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

        

        switch (playerColorNum)
        {
            case 0:
                playerYellow.SetActive(true);
                break;

            case 1:
                playerRed.SetActive(true);
                charaImage.sprite = playerSprites[1];
                break;

            case 2:
                playerBlue.SetActive(true);
                charaImage.sprite = playerSprites[2];
                break;
        }

        charaImage.sprite = playerSprites[playerColorNum];
        timerImage.sprite = timerSprites[playerColorNum];
        countdownImage.sprite = countdownSprites[playerColorNum];
        cloudImage.sprite = cloudSprites[playerColorNum];

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
        score += scoreBasePoint * 2;
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
            

            if(comboTimes < 10){
                audioSource.PlayOneShot(matchSE);
            }else{
                audioSource.PlayOneShot(maxComboSE);
            }
        }
        else
        {
            comboTimes = 0;
            isCombo = false;

            audioSource.PlayOneShot(missSE);
        }

        audioSource.pitch = 1.0f;
        score += (5 * addPoint + comboTimes) * scoreBasePoint / 5;

        
    }


    public void Ranking()
    {
        score += playerColorNum;

        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);

        gameManager.GetComponent<AudioSource>().clip = rankingBGM;
        gameManager.GetComponent<AudioSource>().Play();


    }

}
