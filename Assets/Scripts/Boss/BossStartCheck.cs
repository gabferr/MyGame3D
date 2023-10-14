using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string tagToCheck="Player";

    public Color gizmoColor = Color.yellow;
    public GameObject bossCamera;
    private void Awake()
    {
        bossCamera.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tagToCheck)
        {
            TurnCameraOn();
        }
    }
    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, transform.localScale.x);
    }
}
