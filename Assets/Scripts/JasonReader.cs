using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JasonReader : MonoBehaviour
{
    [SerializeField] TextAsset JasonFile;
    public int _stage;
    public LevelList levels = new LevelList();

    // Singleton instance.
    public static JasonReader Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        // If there is not already an instance , set it to this.
        if (Instance == null)
        {
            Instance = this;
        }
        //If an instance already exists, destroy whatever this object is to enforce the singleton.
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class Level
    {
        public int Round;
        public int Digits; // number of digits
        public int Tries;
    }

    [System.Serializable]
    public class LevelList
    {
        public Level[] level;
    }

    // Start is called before the first frame update
    void Start()
    {
        levels = JsonUtility.FromJson<LevelList>(JasonFile.text);
    }

}
