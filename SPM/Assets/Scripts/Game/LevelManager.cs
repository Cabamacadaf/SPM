using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    //public List<GameObject> ActiveObjects = new List<GameObject>();
    public Dictionary<int, GameObject> AllPickUpObjects = new Dictionary<int, GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void Save()
    {
        SaveSystem.SaveObjects(AllPickUpObjects);
    }



    //public void Load()
    //{

    //    ObjectsData objectsData = SaveSystem.LoadObjects();
    //    for (int i = 0; i < objectsData.activeObjects.Count; i++)
    //    {
    //        PickUpData data = objectsData.activeObjects[i];
    //        GameObject current = ActiveObjects[i].gameObject;


    //        if (data != null)
    //        {

    //            Vector3 position;
    //            position.x = data.position[0];
    //            position.y = data.position[1];
    //            position.z = data.position[2];
    //            current.transform.position = position;

    //            Vector3 rotation;
    //            rotation.x = data.rotation[0];
    //            rotation.y = data.rotation[1];
    //            rotation.z = data.rotation[2];
    //            current.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    //        }
    //    }
    //}


}
