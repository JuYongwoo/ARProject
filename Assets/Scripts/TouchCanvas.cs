using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class TouchCanvas : MonoBehaviour
{

    public enum Objects
    {
        Button,
        Canvas
    }
    private Dictionary<Objects, GameObject> partMap = new Dictionary<Objects, GameObject>();

    void Start()
    {
        CacheParts();
        partMap[Objects.Canvas].GetComponent<Canvas>().worldCamera = Camera.main;
        partMap[Objects.Button].GetComponent<Button>().onClick.AddListener(() => { Destroy(this.transform.root.gameObject); });
    }

    void LateUpdate()
    {
        
        if (partMap.TryGetValue(Objects.Canvas, out GameObject cv))
        {
            cv.transform.LookAt(Camera.main.transform);
            cv.transform.Rotate(0f, 180f, 0f);

        }
        
    }


    private void CacheParts()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (Enum.TryParse(child.name, out Objects part))
            {
                if (!partMap.ContainsKey(part))
                {
                    partMap[part] = child.gameObject;
                }
                else
                {
                    Debug.LogWarning($"[TentacleController] Duplicate part name: {part}");
                }
            }
        }
    }
}
