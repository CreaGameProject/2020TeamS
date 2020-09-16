using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{

    Animator animator;

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

 


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Run", true);

        panel = new GameObject[4];


        Invoke("PanelChecker", 1.0f);
    }



    private void Update()
    {

        //StagePanelSelect
        Ray mouseCursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(mouseCursorRay,out mouseCursorHit))
        {
            if(mouseCursorHit.collider.gameObject.tag == "Panel")
            {
                if (mouseCursorHit.collider.gameObject.GetComponent<PanelScript>().Selectable)
                {
                    Vector3 targetDir = mouseCursorHit.collider.gameObject.transform.position - transform.position;
                    float targetAngle = Mathf.Atan2(targetDir.z, targetDir.x);
                    targetAngle = -1 * Mathf.Rad2Deg * targetAngle + 90f;

                    transform.rotation = Quaternion.Euler(0, targetAngle, 0);

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
                        if(targetAngle <= 40.0f && targetAngle >= -40.0f)
                        {
                            RaycastHit hitInfo;
                            for(int i = 0; i < 5; i++)
                            {
                                if(Physics.Raycast(new Vector3(0.4375f + i * 1.75f,1f,transform.position.z),Vector3.down, out hitInfo, 20f))
                                {
                                    if(hitInfo.collider.gameObject.tag == "Panel")
                                    {
                                        hitInfo.collider.gameObject.GetComponent<PanelScript>().PanelScaleUP(false);
                                    }
                                }
                            }
                           
                            stageManager.GetComponent<StageManager>().StartCoroutine("CreateNewPanel");
                            
                        }

                    }
                }
            }
        }



        if (!isMoving) {
            moveStart.transform.position = transform.position;
            moveGoal.transform.position = transform.position + transform.forward * panelDistance;
        }
        else
        {
            moveTimer += Time.deltaTime;
            float location = moveTimer * moveSpeed / panelDistance;
            transform.position = Vector3.Lerp(moveStart.transform.position, moveGoal.transform.position, location);

            if (transform.position == moveGoal.transform.position)
            {
                PanelChecker();
            }
        }

        stageCamera.transform.position = new Vector3(stageCamera.transform.position.x, stageCamera.transform.position.y,transform.position.z - 6.5f);

    }


    void PanelChecker()
    {
        animator.SetBool("Jump", false);
        isMoving = false;
        moveTimer = 0f;

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
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Panel")
        {
            collision.gameObject.GetComponent<PanelScript>().PanelScaleUP(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "ItemChangeColor")
        {
            stageManager.GetComponent<StageManager>().AllPanelChangeColor();
            Destroy(other.gameObject);
        }
    }


    public void OnCallChangeFace()
    {

    }



}
