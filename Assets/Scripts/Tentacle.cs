using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Tentacle : MonoBehaviour
{

    public enum TentacleParts
    {
        TipSpike,
        TipButton,
        TipCanvas
    }
    private Dictionary<TentacleParts, GameObject> partMap = new Dictionary<TentacleParts, GameObject>();

    void Start()
    {
        CacheParts();
        partMap[TentacleParts.TipCanvas].GetComponent<Canvas>().worldCamera = Camera.main;
        partMap[TentacleParts.TipButton].GetComponent<Button>().onClick.AddListener(() => { Destroy(this.gameObject); });

    }

    void LateUpdate()
    {
        if (partMap.TryGetValue(TentacleParts.TipButton, out GameObject btn))
        {
            btn.transform.LookAt(Camera.main.transform);
        }
    }

    public void Retract()
    {
        Destroy(gameObject);
    }

    private void CacheParts()
    {
        Transform[] children = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in children)
        {
            if (Enum.TryParse(child.name, out TentacleParts part))
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
