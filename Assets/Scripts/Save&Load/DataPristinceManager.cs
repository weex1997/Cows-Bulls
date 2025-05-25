using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPristinceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;

    private List<IDataPrisistence> dataPrisistsObject;

    private FileDataHandler fileDataHandler;
    public bool itsNewGame;
    public bool runOnce = false;
    public bool hasData;
    [SerializeField] private bool useEncryption;
    public static DataPristinceManager Instance { get; private set; }

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

        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);


    }
    void Start()
    {
        LoadGame();
    }
    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        this.dataPrisistsObject = FindAllDataPersistenceObject();
        if (runOnce)
        {
            if (itsNewGame)
            {
                NewGame();
            }
            else
            {
                LoadGame();
            }

            runOnce = false;
        }
        else
        {
            itsNewGame = true;
        }

    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();
        if (this.gameData == null)
        {
            Debug.Log("No data was Found");
            NewGame();
        }

        foreach (IDataPrisistence dataPrisistenceObj in dataPrisistsObject)
        {
            dataPrisistenceObj.LoadData(gameData);
        }

        if (gameData.hiddenNumber.Count == 0 && gameData.attempts == 0)
            hasData = false;
        else
            hasData = true;

    }

    public void SaveGame()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.totalAttempts > 0)
            {
                foreach (IDataPrisistence dataPrisistenceObj in dataPrisistsObject)
                {
                    dataPrisistenceObj.SaveData(ref gameData);
                }

                fileDataHandler.Save(gameData);
            }
        }
    }

    public void DeleteData()
    {
        fileDataHandler.Delete();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPrisistence> FindAllDataPersistenceObject()
    {
        IEnumerable<IDataPrisistence> dataPrisistencesObject = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPrisistence>();

        return new List<IDataPrisistence>(dataPrisistencesObject);
    }
}
