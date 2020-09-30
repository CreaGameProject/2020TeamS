﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                        charaImageYellow.transform.DOScale(1.3f, 0.5f);
                        charaImageRed.transform.DOScale(1.0f, 0.5f);
                        charaImageBlue.transform.DOScale(1.0f, 0.5f);

                        selectNum = 0;
                        break;

                    case "programer3":
                        charaImageRed.transform.DOScale(1.3f, 0.5f);
                        charaImageYellow.transform.DOScale(1.0f, 0.5f);
                        charaImageBlue.transform.DOScale(1.0f, 0.5f);

                        selectNum = 1;
                        break;

                    case "desiner3":
                        charaImageYellow.transform.DOScale(1.0f, 0.5f);
                        charaImageRed.transform.DOScale(1.0f, 0.5f);
                        charaImageBlue.transform.DOScale(1.3f, 0.5f);

                        selectNum = 2;
                        break;

                    default:
                        charaImageYellow.transform.DOScale(1.0f, 0.5f);
                        charaImageRed.transform.DOScale(1.0f, 0.5f);
                        charaImageBlue.transform.DOScale(1.0f, 0.5f);

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

}
