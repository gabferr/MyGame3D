using Animation;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;


namespace enemy
{
    public class EnemyBase : MonoBehaviour, IDamagable

    {
        public new Collider collider;
        public FlashColor flashColor;
        public new ParticleSystem particleSystem;
        public float starLife = 10f;
        public bool LookAtPlayer = false;

        [SerializeField] private float _currentLife;

        [Header("Animation")]
        [SerializeField] private AnimationBase _animationBase;


        [Header("Start Animation")]
        public float startAnimationDuration = .2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Events")]
        public UnityEvent OnKillEvent;


        private Player _player;

        private void Awake()
        {
            Init();
        }
        private void Start()
        {
           // _player = GetComponent<Player>();
           _player = GameObject.FindObjectOfType<Player>();

        }
        protected void ResetLife()
        {
            _currentLife = starLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if (startWithBornAnimation) BornAnimation();
        }
        protected virtual void kill()
        {
                Onkill();
        }  
        protected virtual void Onkill()
        {
            if(collider != null) collider.enabled = false;
            Destroy(gameObject,3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();

        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleSystem != null) particleSystem.Emit(15 );
            _currentLife -= f;

            if(_currentLife <= 0)
            {
                kill();
            }
        }
        #region ANIMATION   
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationSetupByTrigger(animationType);
        }
        #endregion
    
        public void Damage(float damage)
        {
            //Debug.Log("Damage");
            OnDamage(damage);
        }

        public void Damage(float damage, Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();
            if (p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            if (LookAtPlayer)
                transform.LookAt(_player.transform.position);
        }
    }

}

