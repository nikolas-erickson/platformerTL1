using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";
    public FileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        // use Path.Combine to support multiple OS's
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                // load serialized data from file
                string strFromFile = "";
                // write data to file
                using (FileStream fStrean = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader sReader = new StreamReader(fStrean))
                    {
                        strFromFile = sReader.ReadToEnd();
                    }
                }

                // deserialize data
                loadedData = JsonUtility.FromJson<GameData>(strFromFile);

            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        // use Path.Combine to support multiple OS's
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        Debug.Log("saving to " + fullPath);

        try
        {
            //create directory if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize data to store
            string dataToStore = JsonUtility.ToJson(data, true);

            // write data to file
            using (FileStream fStrean = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter sWriter = new StreamWriter(fStrean))
                {
                    sWriter.Write(dataToStore);
                }
            }

        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to " + fullPath + "\n" + e);
        }
    }
}
