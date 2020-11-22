using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollScript : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    private RectTransform rect;
    private Vector3 contentRectPos;
    private int indexNumber;
    private int scrollIndexNumber;
    private float contentWidth;

    void Start(){
        rect = content.GetComponent<RectTransform>();
        contentWidth = content.GetComponent<GridLayoutGroup>().cellSize.x +  content.GetComponent<GridLayoutGroup>().spacing.x;
        indexNumber = 1;
        scrollIndexNumber = 1;
    }

    void Update(){
        contentRectPos = rect.anchoredPosition;
        rect.offsetMin = new Vector2(contentRectPos.x, 0);

        if(scrollIndexNumber != indexNumber){

            switch(indexNumber){
                case 1:
                    rect.anchoredPosition = new Vector3(0 * contentWidth, 0, 0);
                    break;
                case 2:
                    rect.anchoredPosition = new Vector3(-1 * contentWidth, 0, 0);
                    break;
                case 3:
                    rect.anchoredPosition = new Vector3(-2 * contentWidth, 0, 0);
                    break;
                case 4:
                    rect.anchoredPosition = new Vector3(-3 * contentWidth, 0, 0);
                    break;
            }

            scrollIndexNumber = indexNumber;
        }else{
            if(contentRectPos.x > -0.5f * contentWidth){
                indexNumber = 1;
            }else if(contentRectPos.x > -1.5f * contentWidth){
                indexNumber = 2;
            }else if(contentRectPos.x > -2.5f * contentWidth){
                indexNumber = 3;
            }else{
                indexNumber = 4;
            }
        }
        ScrollButtonIndexChecker();
    }

    public void OnLeftButtonClicked(){
        if(indexNumber > 1){
            indexNumber--;
        }
    }

    public void OnRightButtonClicked(){
        if(indexNumber < 4){
            indexNumber++;
        }
        Debug.Log(indexNumber);
    }

    private void ScrollButtonIndexChecker(){
        if(indexNumber == 1){
            leftButton.SetActive(false);
        }else if(indexNumber == 4){
            rightButton.SetActive(false);
        }else{
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
    }

}
