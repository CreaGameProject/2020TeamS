using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScrollScript : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;

    private RectTransform rect;
    private Vector3 contentRectPos;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private float scrollSpeed = 1.0f;
    private float distance_two;
    private bool isMove;
    private int indexNumber;
    private int scrollIndexNumber;
    private float contentWidth;
    private float slerpTimer;
    public enum SCROLL_TYPE{
        SCROLL,
        BUTTON,
    }

    public SCROLL_TYPE scroll_type;
    

    void Start(){
        rect = content.GetComponent<RectTransform>();
        contentWidth = content.GetComponent<GridLayoutGroup>().cellSize.x +  content.GetComponent<GridLayoutGroup>().spacing.x;
        indexNumber = 1;
        scrollIndexNumber = 1;
        startMarker = new Vector3(0 , 0, 0);
        endMarker = new Vector3(0 , 0, 0);
        scroll_type = SCROLL_TYPE.BUTTON;
    }

    void Update(){
        contentRectPos = rect.anchoredPosition;
        rect.offsetMin = new Vector2(contentRectPos.x, 0);

        if(scrollIndexNumber != indexNumber){
            if(scroll_type == SCROLL_TYPE.BUTTON){
                switch(indexNumber){
                    case 1:
                        endMarker = new Vector3(0 * contentWidth, 0, 0);
                        break;
                    case 2:
                        endMarker = new Vector3(-1 * contentWidth, 0, 0);
                        break;
                    case 3:
                        endMarker = new Vector3(-2 * contentWidth, 0, 0);
                        break;
                    case 4:
                        endMarker = new Vector3(-3 * contentWidth, 0, 0);
                        break;
                }
                //rect.anchoredPosition = endMarker;
                
            }
            startMarker = rect.anchoredPosition;
            distance_two = Vector3.Distance(startMarker,endMarker);
            scrollIndexNumber = indexNumber;
        }else{

            if(scroll_type == SCROLL_TYPE.SCROLL){
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
        }


        ScrollButtonIndexChecker();
        if(scroll_type == SCROLL_TYPE.BUTTON){
            /*
            slerpTimer += Time.deltaTime;
            float present_Location = (slerpTimer * scrollSpeed)/distance_two;
            Debug.Log(slerpTimer);
            if(present_Location < 0.95f){
                rect.anchoredPosition = Vector3.Lerp(startMarker,endMarker,present_Location);
            }else{
                slerpTimer = 0;
            }*/ 
            rect.DOLocalMoveX(endMarker.x,1.0f);

        }
    }

    public void OnLeftButtonClicked(){
        if(indexNumber > 1){
            indexNumber--;
        }
        scroll_type = SCROLL_TYPE.BUTTON;
    }

    public void OnRightButtonClicked(){
        if(indexNumber < 4){
            indexNumber++;
        }
        scroll_type = SCROLL_TYPE.BUTTON;
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

    public void OnScrollValueChanged(){
        scroll_type = SCROLL_TYPE.SCROLL;
    }

}
