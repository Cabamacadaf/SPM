using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject player;



    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        //int i = 0;
        //foreach (GameObject targetGameObject in targets)
        //{
        //    Target target = targetGameObject.GetComponent<Target>();
        //    if (target.activeRobot != null)
        //    {
        //        save.livingTargetPositions.Add(target.position);
        //        save.livingTargetsTypes.Add((int)target.activeRobot.GetComponent<Robot>().type);
        //        i++;
        //    }
        //}
        //save.PlayerTransform = player.transform;
        return save;
    }

    public void SaveGame()
    {
        // 1
        //Save save = CreateSaveGameObject();

        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", player.transform.position.z);

        // 2
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        //bf.Serialize(file, save);
        //file.Close();

        // 3

        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // 1
        //if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        //{


        //    // 2
        //    //BinaryFormatter bf = new BinaryFormatter();
        //    //FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        //    //Save save = (Save)bf.Deserialize(file);
        //    //file.Close();

        //    // 3
        //    //for (int i = 0; i < save.livingTargetPositions.Count; i++)
        //    //{
        //    //    int position = save.livingTargetPositions[i];

        //    //}
        //    //player.transform.position = save.PlayerTransform.position;
        //    //player.transform.rotation = save.PlayerTransform.rotation;

        //    // 4


        //    Debug.Log("Game Loaded");

        //    //Unpause();
        //}
        //else
        //{
        //    Debug.Log("No game saved!");
        //}
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"), PlayerPrefs.GetFloat("PlayerY"), PlayerPrefs.GetFloat("PlayerZ"));
        Debug.Log("Game Loaded");


    }


}
