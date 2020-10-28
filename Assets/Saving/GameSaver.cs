using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSaver : MonoBehaviour
{
    public static UnityEvent OnSave = new UnityEvent();
    public static UnityEvent OnLoad = new UnityEvent();

    void Save()
    {
        OnSave.Invoke();
        PlayerPrefs.Save();

        Debug.Log("Saved!");
    }
    void Load()
    {
        OnLoad.Invoke();

        Debug.Log("Loaded!");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }
}
