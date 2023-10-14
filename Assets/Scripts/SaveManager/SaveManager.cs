using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using GABFERR.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    private SaveSetup _saveSetup;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _saveSetup  = new SaveSetup();
        _saveSetup.lastLevel = 2;
        _saveSetup.playerName = "Gabriel";

    }

    #region SAVE
    [NaughtyAttributes.Button]
    private void Save()
    {
     
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        Debug.Log(setupToJson);

        SaveFile(setupToJson);
    }
    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        Save();
    }
    #endregion

    private void SaveFile(string json)
    {
        string path = Application.dataPath + "/save.txt";
        Debug.Log(path);
        File.WriteAllText(path,json);
       
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }
    [NaughtyAttributes.Button]
    private void SaveLevelFive()
    {
        SaveLastLevel(5);
    }
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
}