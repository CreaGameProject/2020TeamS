using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour
{
    public void TitleToSelect()
    {
        SceneManager.LoadScene("Select");
    }
    
    public void ReLoad()
    {
        SceneManager.LoadScene("Main");
    }
    
}
