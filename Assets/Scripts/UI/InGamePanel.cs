using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{

    private enum InGameObjects
    {
        EnemyCount
    }

    private int lastEnemtCount;
    private int enemyCount = 0;
    private Dictionary<InGameObjects, GameObject> InGameObjectsMap = new Dictionary<InGameObjects, GameObject>();

    private void Awake()
    {
        EnemyBase.enemyCountDelta = (int delta) =>
        {
            enemyCount += delta;
        };
        InitInGameObjectsMap();
    }

    void Update()
    {
        if(lastEnemtCount != enemyCount)
        {
            lastEnemtCount = enemyCount;
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
