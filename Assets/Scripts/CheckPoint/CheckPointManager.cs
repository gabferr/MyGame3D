using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GABFERR.Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;
    public List<CheckPointBase> checkpoints;

    public bool HasCheckpoint()
    {
        return lastCheckPointKey > 0;
    }
    public void SaveCheckPoint(int i)
    {
        if (i > lastCheckPointKey )
        {
            lastCheckPointKey = i;
        }
            
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckPointKey);
        return checkpoint.transform.position;
    }
}
