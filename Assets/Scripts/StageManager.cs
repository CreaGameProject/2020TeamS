using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject plane;

 

    public int length;
    public int width;
    public float distance;

    GameObject panel;

    public List<PanelScript> panelScripts;

    void Start()
    {
        StartCoroutine("StageCreate");
        panelScripts = new List<PanelScript>();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AllPanelChangeColor();
        }
    }


    public void AllPanelChangeColor()
    {
        foreach(PanelScript panelScript in panelScripts)
        {
            if(panelScript != null)
            {
                panelScript.ChangeColor();
            }
        }
    }

    IEnumerator StageCreate()
    {
        for (int dz = 0; dz < length; dz++)
        {
            for (int dx = 0; dx < width; dx++)
            {

                if (dz % 2 == 0)
                {
                    panel = Instantiate(plane, new Vector3(dx * distance, 0, dz * distance * Mathf.Sin(Mathf.PI / 3)), transform.rotation);
                }
                else
                {
                    panel = Instantiate(plane, new Vector3(dx * distance + distance / 2, 0, dz * distance * Mathf.Sin(Mathf.PI / 3)), transform.rotation);
                }

                yield return new WaitForSeconds(1 / 80);

                panelScripts.Add(panel.GetComponent<PanelScript>());

                yield return new WaitForSeconds(1 / 80);
            }
        }
    }


  

    public IEnumerator CreateNewPanel()
    {
        for (int dx = 0; dx < width; dx++)
        {
            if (length % 2 == 0)
            {
                panel = Instantiate(plane, new Vector3(dx * distance, 0, length * distance * Mathf.Sin(Mathf.PI / 3)), transform.rotation);
            }
            else
            {
                panel = Instantiate(plane, new Vector3(dx * distance + distance / 2, 0, length * distance * Mathf.Sin(Mathf.PI / 3)), transform.rotation);
            }

            yield return new WaitForSeconds(1 / 80);

            panelScripts.Add(panel.GetComponent<PanelScript>());

            yield return new WaitForSeconds(1 / 80);

            //panelScripts.RemoveAt(dx);
        }
        length++;
    }

   


}
