using Cloth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamagable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;
    public float damageMultiply = 1f;
    public float timeToDestroy = 3f;
    public Action<HealthBase> onDamage;
    public Action<HealthBase> onKill;
    public List<UiFillUpdate> uiHealthUpdater;

    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        ResetLife();
    }
    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, timeToDestroy);
        }
        onKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float damage)
    {
        _currentLife -= damage * damageMultiply;
        if (_currentLife <= 0)
        {
            Kill();
        }
        UpdateUI();
        onDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUI()
    {
        if(uiHealthUpdater != null)
        {
            uiHealthUpdater.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMuliply(float damage, float duration)
    {
        StartCoroutine(ChangeDamageMultiplyCouroutine(damageMultiply, duration));
    }

    IEnumerator ChangeDamageMultiplyCouroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;
        yield return new WaitForSeconds(duration);
        this.damageMultiply = 1;

    }
}
