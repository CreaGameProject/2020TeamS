using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{

    private Animator animator;
    private AudioSource audioSource;

    [SerializeField] private GameObject moveStart;
    [SerializeField] private GameObject moveGoal;

    [SerializeField] GameObject stageManager;
    [SerializeField] GameObject stageCamera;

    private bool isMoving = false;
    private float moveSpeed = 1.2f;
    private float moveTimer = 0;

    private float panelDistance = 1.75f;
    private GameObject[] panel;
    private RaycastHit mouseCursorHit;
    private Vector3 targetDir = new Vector3(0, 0, 0);

    [SerializeField] private AudioClip jumpSE;
    [SerializeField] private AudioClip getItemSE;
    [SerializeField] private AudioClip cursorSE;




    public enum PlayerColor
    {
        YELLOW,
        RED,
        BLUE
    }

    public PlayerColor playerColor;
    private int playerColorNum;



    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Run", true);

        audioSource = GetComponent<AudioSource>();

        panel = new GameObject[4];

        switch (playerColor)
        {
            case PlayerColor.YELLOW:
                playerColorNum = 0;
                break;
            case PlayerColor.RED:
                playerColorNum = 1;
                break;
            case PlayerColor.BLUE:
                playerColorNum = 2;
                break;

        }

        Invoke("PanelChecker", 1.0f);
    }



    private void Update()
    {
        if (stageManager.GetComponent<StageManager>().isGame)
        {
            if (!isMoving)
            {
                Ray mouseCursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(mouseCursorRay, out mouseCursorHit))
                {
                    if (mouseCursorHit.collider.gameObject.tag == "Panel")
                    {
                        if (mouseCursorHit.collider.gameObject.GetComponent<PanelScript>().selectable)
                        {


                            if (targetDir != mouseCursorHit.collider.gameObject.transform.position - transform.position)
                            {
                                if (targetDir.y == mouseCursorHit.collider.gameObject.transform.position.y - transform.position.y)
                                {
                                    audioSource.PlayOneShot(cursorSE);
                                }


                                targetDir = mouseCursorHit.collider.gameObject.transform.position - transform.position;
                            }


                            float targetAngle = Mathf.Atan2(targetDir.z, targetDir.x);
                            targetAngle = -1 * Mathf.Rad2Deg * targetAngle + 90f;
                            transform.rotation = Quaternion.Euler(0, targetAngle, 0);

                            moveStart.transform.position = transform.position;
                            moveGoal.transform.position = transform.position + transform.forward * panelDistance;




                            //Click to move
                            if (Input.GetMouseButtonDown(0))
                            {
                                animator.SetBool("Jump", true);

                                isMoving = true;

                                for (int i = 0; i < 4; i++)
                                {
                                    if (panel[i] != null)
                                    {
                                        panel[i].GetComponent<PanelScript>().PanelUP(false);
                                    }
                                }

                                //When go forward
                                if (targetAngle <= 32.0f && targetAngle >= -32.0f)
                                {
                                    RaycastHit hitInfo;
                                    for (int i = 0; i < 5; i++)
                                    {
                                        if (Physics.Raycast(new Vector3(0.4375f + i * 1.75f, 1f, transform.position.z), Vector3.down, out hitInfo, 20f))
                                        {
                                            if (hitInfo.collider.gameObject.tag == "Panel")
                                            {
                                                hitInfo.collider.gameObject.GetComponent<PanelScript>().PanelScaleUP(false);
                                            }
                                        }
                                    }

                                    stageManager.GetComponent<StageManager>().StartCoroutine("CreateNewPanel");


                                }

                                RaycastHit raycastHit;
                                if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), Vector3.up, out raycastHit, 20f))
                                {
                                    if (raycastHit.collider.gameObject.tag == "Panel")
                                    {
                                        raycastHit.collider.gameObject.GetComponent<PanelScript>().PanelScaleUP(false);
                                    }
                                }


                                audioSource.PlayOneShot(jumpSE);
                            }
                        }
                    }
                }
            }
            else
            {
                moveTimer += Time.deltaTime;
                float location = moveTimer * moveSpeed / panelDistance;
                transform.position = Vector3.Lerp(moveStart.transform.position, moveGoal.transform.position, location);

                //transform.DOMove(moveGoal.transform.position, 1.2f);


                if (transform.position == moveGoal.transform.position)
                {
                    PanelChecker();
                }
            }



            //StagePanelSelect
            

        }

        

        stageCamera.transform.position = new Vector3(stageCamera.transform.position.x, stageCamera.transform.position.y,transform.position.z - 6.5f);

    }


    void PanelChecker()
    {
        animator.SetBool("Jump", false);
        
        moveTimer = 0f;

        moveStart.transform.position = transform.position;
        moveGoal.transform.position = transform.position + transform.forward * panelDistance;

        //まわりの4つのパネルをチェック
        for (int i = 0; i < 4; i++)
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(new Vector3(transform.position.x + panelDistance * Mathf.Cos(i * Mathf.PI / 3), transform.position.y - 2f, transform.position.z + panelDistance * Mathf.Sin(i * Mathf.PI / 3)),
                Vector3.up, out hitInfo, 20f))
            {
                if (hitInfo.collider.gameObject.tag == "Panel") {
                    panel[i] = hitInfo.collider.gameObject;
                    panel[i].GetComponent<PanelScript>().PanelUP(true);
                }
            }
            else
            {
                panel[i] = null;
            }
        }


        //踏んだパネルの色をチェック

        if (stageManager.GetComponent<StageManager>().isGame)
        {
            RaycastHit hitInfoNowPanel;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), Vector3.up, out hitInfoNowPanel, 20f))
            {
                if (hitInfoNowPanel.collider.gameObject.tag == "Panel")
                {
                    if (hitInfoNowPanel.collider.gameObject.GetComponent<PanelScript>().textureNum == playerColorNum)
                    {
                        
                        stageManager.GetComponent<StageManager>().AddScore(3, true);
                        hitInfoNowPanel.collider.gameObject.GetComponent<PanelScript>().audioSource.pitch = 1.0f + 0.1f * stageManager.GetComponent<StageManager>().comboTimes;
                        hitInfoNowPanel.collider.gameObject.GetComponent<PanelScript>().PanelSE(true);
                    }
                    else
                    {
                        
                        stageManager.GetComponent<StageManager>().AddScore(1, false);
                        hitInfoNowPanel.collider.gameObject.GetComponent<PanelScript>().audioSource.pitch = 1.0f;
                        hitInfoNowPanel.collider.gameObject.GetComponent<PanelScript>().PanelSE(false);
                    }
                }
            }

        }

        isMoving = false;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "ItemChangeColor")
        {
            stageManager.GetComponent<StageManager>().AllPanelChangeColor();
            Destroy(other.gameObject);
            audioSource.PlayOneShot(getItemSE);
        }
    }


    public void OnCallChangeFace()
    {

    }



}
