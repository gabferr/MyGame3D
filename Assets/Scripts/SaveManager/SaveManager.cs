using System.IO;
using UnityEngine;
using GABFERR.Core.Singleton;
using System;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]private SaveSetup _saveSetup;
  
    public int lastlevel;
    public Action<SaveSetup> FileLoaded;
    private string _path;

    public SaveSetup Setup
    {
        get { return _saveSetup; }
    }
    private void Start()
    {
        _path = $"{Application.dataPath + "/save.txt"}";
        Invoke(nameof(Load), .1f);
    }
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
      
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
        _saveSetup.playerName = "Gabriel";

    }
    public void SaveItems()
    {
        _saveSetup.coins = Itens.itemManager.Instance.GetItemByType(Itens.ItemsType.COIN).soInt.value;
        _saveSetup.health = Itens.itemManager.Instance.GetItemByType(Itens.ItemsType.LIFEPACK).soInt.value;
        Save();
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
        SaveItems();
        Save();
    }
    #endregion

    private void SaveFile(string json)
    {
        Debug.Log(_path);
        File.WriteAllText(_path,json); 
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";
        if (File.Exists(_path)) { 
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastlevel = _saveSetup.lastLevel;

        }
        else
        {
            CreateNewSave();
            Save();
        }

        FileLoaded.Invoke(_saveSetup);
 
    }

    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }
   
}

[System.Serializable]
public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public int coins;
    public int health;
}