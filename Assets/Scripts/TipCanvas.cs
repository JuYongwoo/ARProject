using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipCanvas : MonoBehaviour
{
    enum buttons
    {
        LeftButton,
        RightButton,
        CharName
    }

    static public Action changepreparedcharacter;
    private Dictionary<buttons, GameObject> buttonmap = new Dictionary<buttons, GameObject>();

    
    void Awake()
    {
        initmapandmapping();
        assignbuttonevent();
        assignfunc();
    }


    void initmapandmapping()
    {
        buttonmap = new Dictionary<buttons, GameObject>();
        Transform[] childrens = GetComponentsInChildren<Transform>();
        foreach (buttons btn in Enum.GetValues(typeof(buttons)))
        {
            foreach (Transform t in childrens)
            {
                if (t.name == btn.ToString())
                {
                    buttonmap[btn] = t.gameObject;
                    break;
                }
            }
        }

    }

    void assignbuttonevent()
    {

        if (buttonmap.ContainsKey(buttons.LeftButton))
            buttonmap[buttons.LeftButton].GetComponent<Button>().onClick.AddListener(() => { changepreparedcharacter?.Invoke(); });
        if (buttonmap.ContainsKey(buttons.RightButton))
            buttonmap[buttons.RightButton].GetComponent<Button>().onClick.AddListener(() => { changepreparedcharacter?.Invoke(); });


    }

    void assignfunc()
    {

        ARCharacterSpawner.changecharname += changetext;
    }

    void changetext(string str)
    {
        buttonmap[buttons.CharName].GetComponent<Text>().text = str;

    }

}