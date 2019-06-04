using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicColliderType : MonoBehaviour
{

    public enum Mode { Default, Intro, House, Ending }
    public Mode musicType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetMusicType()
    {
        string typeString = "";

        switch (musicType)
        {
            case Mode.Default:
                typeString = "Default";
                break;

            case Mode.Intro:
                typeString = "Intro";
                break;
            case Mode.House:
                typeString = "House";
                break;
            case Mode.Ending:
                typeString = "Ending";
                break;

            default:
                typeString = "";
                break;
        }

        return typeString;

    }

}
