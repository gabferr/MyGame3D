using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GABFERR.statemachine;
using DG.Tweening;
using System;


namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }
    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAmimationDuration = .5f;
        public Ease ease = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttack = .5f;


        public float speed = 5f;
        public List<Transform> wayPoints;

        public HealthBase healthBase;

        private StateMachine<BossAction> stateMachine;

        private void OnValidate()
        {
            if (healthBase == null) healthBase = GetComponent<HealthBase>();
        }
        private void Awake()
        {
            Init();
            OnValidate();
            if(healthBase != null)
            {
            healthBase.onKill += BossOnKill;
            }
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();
            stateMachine.RegisterStates(BossAction.INIT,new BossStateInit()); 
            stateMachine.RegisterStates(BossAction.WALK,new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK,new BossStateAttack()); 
            stateMachine.RegisterStates(BossAction.DEATH,new BossStateDeath());
        }
        private void BossOnKill(HealthBase  h)
        {
            SwitchState( BossAction.DEATH);
        }
        #region ATTACK
        public void StartAttak(Action endCallBack = null)
        {
            StartCoroutine(AttackCoroutine(endCallBack));
        }
        IEnumerator AttackCoroutine(Action endCallBack = null)
        {
            int attacks = 0;
            while(attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttack);
            }
            endCallBack?.Invoke();
        }
        #endregion

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToRandomPointCoroutine(wayPoints[UnityEngine.Random.Range(0,wayPoints.Count)], onArrive));
        }
        IEnumerator GoToRandomPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed) ;
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAmimationDuration).SetEase(ease).From();
        }
        #endregion

        #region DEBUG
        [NaughtyAttributes.Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }[NaughtyAttributes.Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }[NaughtyAttributes.Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        { 
            stateMachine.SwitchStates(state, this);

        }
        #endregion
    }


}


