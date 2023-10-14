using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class ChestBase : MonoBehaviour
{
    public KeyCode KeyCode = KeyCode.F;
    public Animator animator;
    public string triggerOpen = "open";
    private bool _chestOpened = false;


    [Header("Notification")]
    public GameObject notification;
    public float tweenDuration = .2f;
    public Ease easyTween = Ease.OutBack;
    private float startScale;

    [Space]
    public ChestItemBase chestItem;

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpened) return;
        animator.SetTrigger(triggerOpen);
        _chestOpened = true;
        HideNotification();
        Invoke(nameof(showItem), 1f);
    }

    private void showItem()
    {
        chestItem.ShowItem();
        Invoke(nameof(CollectItem), 1f);
    }
    private void CollectItem()
    {
        chestItem.Collect();
    }
    private void OnTriggerEnter(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if(p != null)
        {
            ShowNotification();
        }
    }  
    private void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();
        if(p != null)
        {
            HideNotification();
        }
    }
    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(startScale,tweenDuration);
    }
    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }

    private void Start()
    {
        HideNotification();
        startScale = notification.transform.localScale.x;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }
}
