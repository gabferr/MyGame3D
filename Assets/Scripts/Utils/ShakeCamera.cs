using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GABFERR.Core.Singleton;
using Cinemachine;
public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;
    public float shakeTime;
  public void Shake(float amplitude,float frequency,float time)
    {

    }
}
