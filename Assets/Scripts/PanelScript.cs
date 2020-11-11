using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelScript : MonoBehaviour
{

    

    [SerializeField] private Texture[] textures = new Texture[3];
    [System.NonSerialized] public int textureNum;

    private Renderer targetRenderer;

    [SerializeField] private GameObject item;

    
    [System.NonSerialized] public bool selectable;
    [System.NonSerialized] public bool panelDisappear;

    [SerializeField] private GameObject particle;


    private void Start()
    {


        selectable = false;
        panelDisappear = false;

        int num = Random.Range(0, 100);
        if(num < 10)
        {
            item.SetActive(true);
        }
        
        textureNum = Random.Range(0, 3);

        SetTexture(textureNum);
        PanelScaleUP(true);
    }


    private void Update()
    {
        if (item != null)
        {
            item.transform.Rotate(new Vector3(-15, -30, -45) * Time.deltaTime);
        }
    }

    public void PanelUP(bool up)
    {
        if (up)
        {
            transform.DOLocalMove(new Vector3(transform.position.x, 0.3f, transform.position.z), 0.2f);
            selectable = true;
        }
        else
        {
            transform.DOLocalMove(new Vector3(transform.position.x, 0, transform.position.z), 0.2f);
            selectable = false;
        }
    }

    public void PanelScaleUP(bool up)
    {
        if (up)
        {
            transform.DOScale(new Vector3(1, 1, 1), 0.8f);
        }
        else
        {
            transform.DOScale(new Vector3(0, 0, 0), 0.8f);
            Destroy(this.gameObject, 1.0f);
        }
    }

    

    

    public void SetTexture(int textureNum)
    {
        targetRenderer = this.GetComponent<Renderer>();
        targetRenderer.material.SetTexture("_BaseMap", textures[textureNum]);
    }

    public void ChangeTexture()
    {
        textureNum++;
        if(textureNum > 2)
        {
            textureNum = 0;
        }
        targetRenderer.material.SetTexture("_BaseMap", textures[textureNum]);
    }




    private void Particle()
    {
        particle.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Invoke("Particle", 0.2f);
    }
}
