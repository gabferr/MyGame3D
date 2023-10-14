using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UiFillUpdate> uiGunUpdate;                                        

    public float maxShoot= 5;
    public float timeToRecharge = 1f;

    private float _currentsShoots;
    private bool _recharging = false;

    private void Awake()
    {
        GetAllUis();
    }

    protected override IEnumerator ShootCoroutine()
    {
      
        if(_recharging) yield break;
        while (true)
        {
            if(_currentsShoots < maxShoot)
            {
                Shoot();
                _currentsShoots++;
                CheckRecharge();
                UpdateUi();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentsShoots >= maxShoot)
        {
            StopShoot();
            StartRecharging();
        }
            
    }

    private void StartRecharging()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }
    IEnumerator RechargeCoroutine()
        {
            float time = 0;
            while (time < timeToRecharge) 
            { 
            time += Time.deltaTime;
            uiGunUpdate.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
            }
            _currentsShoots = 0;
            _recharging = false;
        }

    private void UpdateUi()
    {
        uiGunUpdate.ForEach(i => i.UpdateValue(maxShoot, _currentsShoots));
    }

    private void GetAllUis()
    {
        uiGunUpdate = GameObject.FindObjectsOfType<UiFillUpdate>().ToList();
    }
}
