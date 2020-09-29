using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject plane;

    public int length;
    public int width;
    public float distance;

    GameObject panel;

    [System.NonSerialized] public List<PanelScript> panelScripts;


    private float gameTimer = 90.0f;
    [SerializeField] private Text timerText;

    [System.NonSerialized] public bool isCombo;
    private int comboTimes = 0;
    private float comboTimer = 5.0f;
    [System.NonSerialized] public int score = 0;
    [SerializeField] private Text scoreText;




    void Start()
    {
        StartCoroutine("StageCreate");
        panelScripts = new List<PanelScript>();
    }

    
    void Update()
    {
        gameTimer -= Time.deltaTime;

        scoreText.text = "SCORE:" + score.ToString();

        if (isCombo)
        {
            comboTimer -= Time.deltaTime;
            if(comboTimer <= 0.0f)
            {
                isCombo = false;
            }
        }
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
            comboTimer = 5.0f;
        }

        if (combo)
        {
            isCombo = true;
        }
        else
        {
            comboTimes = 0;
            isCombo = false;
        }


        score += addPoint + comboTimes;

        
    }

}
