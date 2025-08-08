using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanelObject : MonoBehaviour
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
        buttonmap = Util.cacheDictionaryUIObjs<buttons>(this.gameObject);
        assignbuttonevent();
        assignfunc();
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

        ARCharacterSpawner.changecharname = changetext;
    }

    void changetext(string str)
    {
        buttonmap[buttons.CharName].GetComponent<Text>().text = str;

    }

}