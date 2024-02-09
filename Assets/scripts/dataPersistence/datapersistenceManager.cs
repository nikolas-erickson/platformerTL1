using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

public class datapersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private FileHandler fHandler;
    private List<iDataPersistence> dataPersistenceObjects;
    [SerializeField] private gameSaveData gameSaveInt;

    public static datapersistenceManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Found an extra Data Persistence Manager");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        this.fHandler = new FileHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        loadGame();
    }
    public void newGame()
    {
        this.gameData = new GameData();
    }
    public void loadGame()
    {
        // load saved data from file using data handler
        this.gameData = fHandler.Load();


        //if no data loaded, initialize a new game
        if (this.gameData == null)
        {
            Debug.Log("No data was found, starting new gameData");
            newGame();
        }
        // push data to scripts that need it
        foreach (iDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.loadData(gameData);
        }
        Debug.Log("loaded data " + gameData.deaths + " " + gameData.levels);
    }
    public void saveGame()
    {
        // pass data to other scripts so they can update id
        foreach (iDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.saveData(ref gameData);
        }

        // pass data to savefile via data handler
        fHandler.Save(gameData);


        Debug.Log("saved data " + gameData.deaths + " " + gameData.levels);
    }

    private void OnApplicationQuit()
    {
        saveGame();
    }

    private List<iDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<iDataPersistence> objs = FindObjectsOfType<MonoBehaviour>().OfType<iDataPersistence>();
        List<iDataPersistence> returnValue = new List<iDataPersistence>(objs);
        returnValue.Add(gameSaveInt);
        return returnValue;
    }
}
