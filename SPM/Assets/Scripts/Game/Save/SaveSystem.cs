using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public static class SaveSystem
{
    public static string playerPath = Application.persistentDataPath + "/player.save";
    public static string checkpointPath = Application.persistentDataPath + "/checkpoints.save";
    public static string objectsPath = Application.persistentDataPath + "/Objects.fun";



    public static void SavePlayer (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerPath, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(playerPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(playerPath, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
           //Debug.LogError("Save file not found in " + playerPath);
            return null;
        }
    }

    public static void SaveCheckpoint(CheckpointTrigger checkpoint)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(checkpointPath, FileMode.Create);

        CheckpointData data = new CheckpointData(checkpoint);
        Debug.Log(checkpointPath);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CheckpointData LoadCheckpoint()
    {
        if (File.Exists(checkpointPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(checkpointPath, FileMode.Open);

            CheckpointData data = formatter.Deserialize(stream) as CheckpointData;
            stream.Close();

            return data;
        }
        else
        {
           // Debug.LogError("Save file not found in " + checkpointPath);
            return null;
        }
    }

    public static void SaveObjects(Dictionary<int, GameObject> AllPickUpObjects)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(objectsPath, FileMode.Create);

        foreach (GameObject currentObject in AllPickUpObjects.Values)
        {
            PickUpData currentData = new PickUpData(currentObject);

            string currentObjectInfo = currentData + ",";

            formatter.Serialize(stream, currentObjectInfo);
        }


        stream.Close();
    }

    public static void LoadObjects()
    {
        Debug.Log("LoadObjects");

        if (File.Exists(objectsPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(objectsPath, FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string line = reader.ReadLine();
            int i = 0;
          
            while (reader.Read() != -1)
            {
                PickUpData data = formatter.Deserialize(stream) as PickUpData;
                Debug.Log("Line " + i + ": " + data.position[0] + "\n");

            }

            stream.Close();


        }
        else
        {
            Debug.LogError("Save file not found in " + playerPath);

        }
    }

    public static void DeleteFile()
    {
        File.Delete(playerPath);
        File.Delete(checkpointPath);
        File.Delete(objectsPath);
    }

}
