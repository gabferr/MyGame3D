using System.Collections.Generic;
using UnityEngine;
using GABFERR.Core.Singleton;
using System.Collections;
using Cloth;

public class Player : Singleton<Player>//, IDamagable
{
    public List<Collider> colliders;
    public Animator animator;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    [Header("Speed Setup")]
    public KeyCode KeyRun = KeyCode.LeftShift;
    public float SpeedRun = 1.5f;
    private float vSpeed = 0f;
    [Header("Flash")]
    public List<FlashColor> flashColors;
    [Header("Life")]
    public HealthBase healthBase;
    public UiFillUpdate uiGunUpdate;

    [Space]
    [SerializeField]private ClothChange _clothChange;

    public CharacterController characterController;

    private bool _alive = true;

    protected override void Awake()
    {
        base.Awake();
        OnValidate();
        healthBase.onDamage += Damage;
        healthBase.onKill += OnKill;
    }
    private void OnValidate()
    {
        if (healthBase == null) 
        { healthBase = GetComponent<HealthBase>(); }
    }

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            vSpeed = 0;
            var isGrounded = characterController.isGrounded;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vSpeed = jumpSpeed;
                animator.SetBool("Jump", isGrounded);
            }
        }
        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(KeyRun))
            {
                speedVector *= SpeedRun;
                animator.speed = SpeedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }
        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;
        characterController.Move(speedVector * Time.deltaTime * turnSpeed);
        animator.SetBool("Run", isWalking);

    }
    [NaughtyAttributes.Button]
    public void Respawn()
    {
        if (CheckPointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckPointManager.Instance.GetPositionFromLastCheckpoint();
        }

    }
    public void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), .1f);
    }
    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }
    #region LIFE

    private void OnKill(HealthBase h)
    {
        if (_alive )
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            Invoke(nameof(Revive), 3f);
        }
    }

    public void Damage(HealthBase h)
    {

        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();

    }
    /*
    public void Damage(float damage, Vector3 dir)
    {
    }*/
    #endregion
    public void changeSpeed(float speed,float duration)
    {
        StartCoroutine(ChangeSpeedCouroutine(speed, duration)); 
    }
   IEnumerator ChangeSpeedCouroutine(float Localspeed, float duration) { 
        var defaultSpeed = speed;
        speed = Localspeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCouroutine(setup, duration));
    }

    IEnumerator ChangeTextureCouroutine(ClothSetup setup, float duration)
    {
        _clothChange.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChange.ResetTexture();        
    }



}
