using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManagerScript : MonoBehaviour
{
    [SerializeField] private AudioClip titleBGM;

    private void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager != null)
        {

            gameManager.GetComponent<GameManagerScript>().audioSource.clip = titleBGM;
            gameManager.GetComponent<GameManagerScript>().audioSource.Play();
        }
    }
}
