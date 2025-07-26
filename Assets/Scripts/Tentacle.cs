using UnityEngine;
using System;
using System.Collections.Generic;

public class Tentacle : MonoBehaviour
{

    public enum TentacleParts
    {
        TipSpike,
        TipButton
    }
    private Dictionary<TentacleParts, GameObject> partMap = new Dictionary<TentacleParts, GameObject>();

    void Awake()
    {
        CacheParts();
    }

    void LateUpdate()
    {
        if (partMap.TryGetValue(TentacleParts.TipSpike, out GameObject tip) &&
            partMap.TryGetValue(TentacleParts.TipButton, out GameObject tipButton))
        {
            tipButton.transform.position = tip.transform.position;
            tipButton.transform.LookAt(Camera.main.transform);
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
