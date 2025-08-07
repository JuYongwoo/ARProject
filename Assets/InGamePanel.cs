using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{

    private enum InGameObjects
    {
        EnemyCount
    }
    // Start is called before the first frame update
    public static int EnemyCount = 0;
    private int lastEnemtCount;
    private Dictionary<InGameObjects, GameObject> InGameObjectsMap = new Dictionary<InGameObjects, GameObject>();

    private void Awake()
    {
        InitInGameObjectsMap();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lastEnemtCount != EnemyCount)
        {
            lastEnemtCount = EnemyCount;
            InGameObjectsMap[InGameObjects.EnemyCount].GetComponent<Text>().text = "³²Àº Àû: " + lastEnemtCount;

        }
        
    }

    private void InitInGameObjectsMap()
    {
        InGameObjectsMap.Clear();
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (InGameObjects arrow in Enum.GetValues(typeof(InGameObjects)))
        {
            foreach (Transform child in children)
            {
                if (child.name == arrow.ToString())
                {
                    InGameObjectsMap[arrow] = child.gameObject;
                    break;
                }
            }
        }
    }
}
