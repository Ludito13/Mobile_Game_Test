using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [SerializeField] public GameData _data;

    string pathSave;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        pathSave = Application.persistentDataPath + "/GameData.json";

        Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_data);

        File.WriteAllText(pathSave, json);
    }

    public void Load()
    {
        if (!File.Exists(pathSave)) return;

        string json = File.ReadAllText(pathSave);

        _data = JsonUtility.FromJson<GameData>(json);
    }
}
