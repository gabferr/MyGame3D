using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkPointActive = false;
    public string checkPoint = "CheckPoint";
    private void OnTriggerEnter(Collider other)
    {
        if(!checkPointActive && other.transform.tag == "Player")
        {
        CheckCheckPoint();
        }
    }
    private void CheckCheckPoint()
    {
        SaveCheckPoint();
        TurnOn();
    }

    [NaughtyAttributes.Button]
    private void TurnOn()
    {
        meshRenderer.material.SetColor("_EmissionColor",Color.white);
    }
    private void TurnOff()
    {
        meshRenderer.material.SetColor("_EmissionColor",Color.grey);
    }
    private void SaveCheckPoint()
    {
        /* if (PlayerPrefs.GetInt(checkPoint,0) > key)
             PlayerPrefs.SetInt(checkPoint,key);
         ;*/
        CheckPointManager.Instance.SaveCheckPoint(key);
        checkPointActive = true;
    }
}
