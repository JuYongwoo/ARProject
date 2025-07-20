using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    enum buttons
    {
        LeftButton,
        RightButton
    }

    static public event Action changepreparedcharacter;
    private Dictionary<buttons, GameObject> buttonmap = new Dictionary<buttons, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] childrens = GetComponentsInChildren<GameObject>();
        List<GameObject> list = new List<GameObject>();
        list.AddRange(childrens);

        foreach (GameObject go in list)
        {
            if(go.name == Enum.GetName(typeof(buttons), buttons.LeftButton))
            {
                buttonmap[buttons.LeftButton] = go;
            }
            else if(go.name == Enum.GetName(typeof(buttons), buttons.RightButton))
            {
                buttonmap[buttons.RightButton] = go;
            }
        }



        buttonmap[buttons.LeftButton].GetComponent<Button>().onClick.AddListener(() => { changepreparedcharacter(); });
        buttonmap[buttons.RightButton].GetComponent<Button>().onClick.AddListener(() => { changepreparedcharacter(); });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
