using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public class itemCollactableBase : MonoBehaviour
    {
        public ItemsType itemsType;

        public string compareTag = "Player";
        public new ParticleSystem particleSystem;
        public int timeToHide = 3;
        public GameObject graphicItem;

        public new Collider collider;

        [Header("Sounds")]
        public AudioSource audioSorce;
        private void Awake()
        {
            // if (particleSystem != null) particleSystem.transform.SetParent(null);
        }
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.transform.CompareTag(compareTag))
            {
                Collect();
            }

        }
        protected virtual void Collect()
        {
            if(collider != null) collider.enabled = false;
            if (graphicItem != null) graphicItem.SetActive(false);
            Invoke("HideObject", timeToHide);
            OnCollect();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
        protected virtual void OnCollect()
        {
            if (particleSystem != null) particleSystem.Play();
            if (audioSorce != null) audioSorce.Play();
            itemManager.Instance.AddByType(itemsType);
        }

    }
}

