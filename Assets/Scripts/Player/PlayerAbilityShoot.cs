using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<UiFillUpdate> uiGunUpdate;

    public GunBase gunBase;
    public Transform gunPosition;
    public FlashColor flashColor;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();
        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.performed += cts => CancelShoot();
    }
   
    public void CreateGun()
    {
        _currentGun = Instantiate(gunBase,gunPosition);
        _currentGun.transform.localEulerAngles = _currentGun.transform.localPosition = Vector3.zero;
    }
    private void StartShoot()
    {
        _currentGun.StartShoot();
        flashColor?.Flash();
        Debug.Log("Start Shoot"); 
    }private void CancelShoot()
    {
        _currentGun.StopShoot();
        Debug.Log("Cancel Shoot"); 
    }
}
