using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class CharaSelectScript : MonoBehaviour
{

    [SerializeField] private GameObject charaImageYellow;
    private Vector3 yellowDefaultPos;
    [SerializeField] private GameObject charaImageRed;
    private Vector3 redDefaultPos;
    [SerializeField] private GameObject charaImageBlue;
    private Vector3 blueDefaultPos;

    private bool isSelect = false;
    private int selectNum;

    [SerializeField] private GameObject UIPanel;

    private RaycastHit mouseCursorHit;


    private float normalScale = 2.0f;
    private float selectScale = 2.6f;
    private float selectTime = 0.5f;


    private void Start()
    {
        yellowDefaultPos = charaImageYellow.transform.position;
        redDefaultPos = charaImageRed.transform.position;
        blueDefaultPos = charaImageBlue.transform.position;
    }

    private void Update()
    {


        if (!isSelect)
        {
            Ray mouseCursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseCursorRay, out mouseCursorHit))
            {
                switch (mouseCursorHit.collider.gameObject.name)
                {
                    case "sound3":
                        charaImageYellow.transform.DOScale(selectScale, selectTime);
                        charaImageRed.transform.DOScale(normalScale, selectTime);
                        charaImageBlue.transform.DOScale(normalScale, selectScale);

                        selectNum = 0;
                        break;

                    case "programer3":
                        charaImageRed.transform.DOScale(selectScale, selectTime);
                        charaImageYellow.transform.DOScale(normalScale, selectTime);
                        charaImageBlue.transform.DOScale(normalScale, selectTime);

                        selectNum = 1;
                        break;

                    case "desiner3":
                        charaImageYellow.transform.DOScale(normalScale, selectTime);
                        charaImageRed.transform.DOScale(normalScale, selectTime);
                        charaImageBlue.transform.DOScale(selectScale, selectTime);

                        selectNum = 2;
                        break;

                    default:
                        charaImageYellow.transform.DOScale(normalScale, selectTime);
                        charaImageRed.transform.DOScale(normalScale, selectTime);
                        charaImageBlue.transform.DOScale(normalScale, selectTime);

                        selectNum = -1;
                        break;
                }

                if (Input.GetMouseButtonDown(0) && selectNum != -1)
                {
                    Invoke("UIPanelSetActive", 0.5f);
                    isSelect = true;
                }
            }
        }
        else
        {
            switch (selectNum)
            {
                case 0:
                    charaImageYellow.transform.DOMoveX(blueDefaultPos.x, 0.5f);
                    charaImageRed.transform.DOMoveX(blueDefaultPos.x + 16, 0.5f);
                    charaImageBlue.transform.DOMoveX(blueDefaultPos.x + 16, 0.5f);
                    break;

                case 1:
                    charaImageYellow.transform.DOMoveX(blueDefaultPos.x + 16, 0.5f);
                    charaImageRed.transform.DOMoveX(blueDefaultPos.x, 0.5f);
                    charaImageBlue.transform.DOMoveX(blueDefaultPos.x + 16, 0.5f);
                    break;

                case 2:
                    charaImageYellow.transform.DOMoveX(blueDefaultPos.x + 16, 0.5f);
                    charaImageRed.transform.DOMoveX(blueDefaultPos.x + 16, 0.5f);
                    charaImageBlue.transform.DOMoveX(blueDefaultPos.x, 0.5f);
                    break;
            }


        }
    }

    private void UIPanelSetActive()
    {
        UIPanel.SetActive(true);
    }

    public void NoButton()
    {
        UIPanel.SetActive(false);

        isSelect = false;

        charaImageYellow.transform.DOMoveX(yellowDefaultPos.x, 0.5f);
        charaImageRed.transform.DOMoveX(redDefaultPos.x, 0.5f);
        charaImageBlue.transform.DOMoveX(blueDefaultPos.x, 0.5f);
    }

    public void YesButton()
    {
        GameObject gameManager = GameObject.Find("GameManager");

        if (gameManager != null)
        {
            gameManager.GetComponent<GameManagerScript>().playerNumber = selectNum;
        }
        else
        {
            Debug.Log("GameManagerが見つかりません。");
        }

        SceneManager.LoadScene("Main");
    }

}
