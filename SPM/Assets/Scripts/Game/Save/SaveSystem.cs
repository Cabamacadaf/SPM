using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;

public static class SaveSystem
{
    public static string playerPath = Application.persistentDataPath + "/player.save";
    public static string checkpointPath = Application.persistentDataPath + "/checkpoints.save";
    public static string objectsPath = Application.persistentDataPath + "/Objects.save";
    public static string enemyPath = Application.persistentDataPath + "/Enemies.save";
    public static string destructibleObjectPath = Application.persistentDataPath + "/Destructible.save";
    public static string healthpackPath = Application.persistentDataPath + "/Healthpack.save";


    public static void SavePlayer (Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(playerPath, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer ()
    {
        if (File.Exists(playerPath)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(playerPath, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else {
            //Debug.LogError("Save file not found in " + playerPath);
            return null;
        }
    }

    public static void SaveCheckpoint (CheckpointTrigger checkpoint)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(checkpointPath, FileMode.Create);

        CheckpointData data = new CheckpointData(checkpoint);
        Debug.Log(checkpointPath);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CheckpointData LoadCheckpoint ()
    {
        if (File.Exists(checkpointPath)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(checkpointPath, FileMode.Open);

            CheckpointData data = formatter.Deserialize(stream) as CheckpointData;
            stream.Close();

            return data;
        }
        else {
            // Debug.LogError("Save file not found in " + checkpointPath);
            return null;
        }
    }

    public static void SaveObjects (Dictionary<int, GameObject> AllPickUpObjects)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(objectsPath, FileMode.Create);
        ObjectsData allData = new ObjectsData();

        foreach (GameObject currentObject in AllPickUpObjects.Values) {
            PickUpData currentData = new PickUpData(currentObject);

            allData.activeObjects.Add(currentData);
            //string currentObjectInfo = currentData + ",";

        }

        formatter.Serialize(stream, allData);

        stream.Close();
    }

    public static void LoadObjects ()
    {
        Debug.Log("LoadObjects");

        if (File.Exists(objectsPath)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(objectsPath, FileMode.Open);
            //StreamReader reader = new StreamReader(stream);
            //string line = reader.ReadLine();
            //int i = 0;

            ObjectsData allData = (ObjectsData)formatter.Deserialize(stream);
            stream.Close();

            foreach (PickUpData data in allData.activeObjects) {
                if (LevelManager.Instance.AllPickUpObjects.ContainsKey(data.ID) == true) {
                    LevelManager.Instance.AllPickUpObjects.TryGetValue(data.ID, out GameObject gameObject);
                    gameObject.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
                    gameObject.transform.rotation = Quaternion.Euler(data.rotation[0], data.rotation[1], data.rotation[2]);
                }
                Debug.Log("Data: " + data);
            }
        }
        else {
            Debug.LogError("Save file not found in " + playerPath);

        }
    }

    public static void SaveEnemies (Dictionary<int, Enemy> AllEnemies)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(enemyPath, FileMode.Create);
        ObjectsData allData = new ObjectsData();

        foreach (Enemy currentEnemy in AllEnemies.Values) {
            EnemyData currentData = new EnemyData(currentEnemy);

            allData.activeEnemies.Add(currentData);

        }

        formatter.Serialize(stream, allData);

        stream.Close();
    }

    public static void LoadEnemies ()
    {
        if (File.Exists(enemyPath)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(enemyPath, FileMode.Open);

            ObjectsData allData = (ObjectsData)formatter.Deserialize(stream);
            stream.Close();

            foreach (EnemyData data in allData.activeEnemies) {
                if (LevelManager.Instance.AllEnemies.ContainsKey(data.ID) == true) {
                    LevelManager.Instance.AllEnemies.TryGetValue(data.ID, out Enemy enemy);
                    enemy.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
                    enemy.transform.rotation = Quaternion.Euler(data.rotation[0], data.rotation[1], data.rotation[2]);
                    if (data.dead == 1) {
                        enemy.gameObject.SetActive(false);
                    }
                    else {
                        enemy.gameObject.SetActive(true);
                    }
                    Transform aggroRange = enemy.transform.Find("Aggro Range");
                    if (data.aggro == 1) {
                        aggroRange.gameObject.SetActive(false);
                        if (enemy is Enemy1) {
                            enemy.Transition<Enemy1AggroState>();
                        }
                        else {
                            enemy.Transition<Enemy2AggroState>();
                        }
                    }
                    else {
                        aggroRange.gameObject.SetActive(true);
                        if (enemy is Enemy1) {
                            enemy.Transition<Enemy1IdleState>();
                        }
                        else {
                            enemy.Transition<Enemy2IdleState>();
                        }
                    }
                }
            }
        }
        else {
            Debug.LogError("Save file not found in " + playerPath);
        }
    }

    public static void SaveDestructibleObjects (Dictionary<int, DestructibleObject> AllDestructibleObjects)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(destructibleObjectPath, FileMode.Create);
        ObjectsData allData = new ObjectsData();

        foreach (DestructibleObject gameObject in AllDestructibleObjects.Values) {
            DestructibleObjectData currentData = new DestructibleObjectData(gameObject);

            allData.activeDestructibleObjects.Add(currentData);
        }


        formatter.Serialize(stream, allData);

        stream.Close();
    }

    public static void LoadDestructibleObjects ()
    {
        if (File.Exists(destructibleObjectPath)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(destructibleObjectPath, FileMode.Open);

            ObjectsData allData = (ObjectsData)formatter.Deserialize(stream);
            stream.Close();

            foreach (DestructibleObjectData data in allData.activeDestructibleObjects) {
                if (LevelManager.Instance.AllDestructibleObjects.ContainsKey(data.ID) == true) {
                    LevelManager.Instance.AllDestructibleObjects.TryGetValue(data.ID, out DestructibleObject destructibleObject);
                    if(data.destroyed == 1) {
                        destructibleObject.HitPoints = destructibleObject.StartHitPoints;
                        destructibleObject.gameObject.SetActive(true);
                    }
                    else {
                        destructibleObject.gameObject.SetActive(false);
                    }
                }
            }
        }
        else {
            Debug.LogError("Save file not found in " + playerPath);
        }
    }

    public static void SaveHealthPack(Dictionary<int, HealthPack> AllHealthPacks)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(healthpackPath, FileMode.Create);
        ObjectsData allData = new ObjectsData();

        foreach (HealthPack gameObject in AllHealthPacks.Values)
        {
            HealthPackData currentData = new HealthPackData(gameObject);

            allData.activeHealthPacks.Add(currentData);
        }

        formatter.Serialize(stream, allData);
        stream.Close();
    }

    public static void LoadHealthPacks()
    {
        if (File.Exists(healthpackPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(healthpackPath, FileMode.Open);

            ObjectsData allData = (ObjectsData)formatter.Deserialize(stream);
            stream.Close();

            foreach (HealthPackData data in allData.activeHealthPacks)
            {
                if (LevelManager.Instance.AllHealthPacks.ContainsKey(data.ID) == true)
                {
                    LevelManager.Instance.AllHealthPacks.TryGetValue(data.ID, out HealthPack healthPack);
                    if (data.used == 1)
                    {
                        //.HitPoints = destructibleObject.StartHitPoints;
                        healthPack.gameObject.SetActive(true);
                    }
                    else
                    {
                        healthPack.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + playerPath);
        }
    }


    public static void DeleteFile ()
    {
        File.Delete(playerPath);
        File.Delete(checkpointPath);
        File.Delete(objectsPath);
        File.Delete(destructibleObjectPath);
        File.Delete(enemyPath);
        File.Delete(healthpackPath);
    }

}
