using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [System.NonSerialized] public static int playerNumber = 0;



    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Title");
    }



}
