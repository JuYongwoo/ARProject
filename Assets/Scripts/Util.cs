using System;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    public static Dictionary<T, GameObject> cacheDictionaryUIObjs<T>(GameObject go)
    {
        Dictionary<T, GameObject> goDict = new Dictionary<T, GameObject>();

        Transform[] children = go.GetComponentsInChildren<Transform>(true);

        foreach (T enumName in Enum.GetValues(typeof(T)))
        {
            foreach (Transform child in children)
            {
                if (child.name == enumName.ToString())
                {
                    goDict[enumName] = child.gameObject;
                    break;
                }
            }
        }

        return goDict;
    }

}