using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARCharacterSpawner : MonoBehaviour
{
    [Header("ĳ���� ������")]
    public GameObject characterPrefab;

    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedCharacter;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // ĳ���� �̹� ��ȯ�Ǿ����� ����
        if (spawnedCharacter != null)
            return;

        // ��ġ ����
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        // ȭ���� ��ġ���� �� ���� �����ϴ��� Ȯ��
        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // ĳ���� ��ȯ
                spawnedCharacter = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}
