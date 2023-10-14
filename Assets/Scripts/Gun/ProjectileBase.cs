using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{

    public float speed = 50f;
    public int damageAmount = 1;

    public List<string> tags;
    
    public float timeToDestroy = 2f;

    private void Awake()
    {
        Destroy(gameObject,timeToDestroy);
    }
    private void Update()
    {
      transform.Translate(Vector3.forward * Time.deltaTime * speed );
    }
   

    private void OnCollisionEnter(Collision collision)
    {
        foreach(var t  in tags)
        {
             if(collision.transform.tag == t) 
            { 
       
                var damageable = collision.transform.GetComponent<IDamagable>();

                if (damageable != null) 
                { 
                    Vector3 dir  = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;

                    damageable.Damage(damageAmount,dir);
                }
                break;
            }
        }
        Destroy(gameObject);
    }

}
