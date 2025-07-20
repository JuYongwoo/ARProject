using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARCharacterSpawner : MonoBehaviour
{

    enum Prefabs : int
    {
        dog,
        unitychan,
        count
    }


    [Header("ĳ���� ������")]
    public GameObject characterPrefab;

    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private Dictionary<Prefabs, GameObject> prefabmap = new Dictionary<Prefabs, GameObject>();
    private GameObject spawnedCharacter;
    private Prefabs nowindex = Prefabs.dog;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();

        prefabmap.Add(Prefabs.dog, Resources.Load<GameObject>("Prefabs/DogPolyart"));
        prefabmap.Add(Prefabs.unitychan, Resources.Load<GameObject>("Prefabs/unitychan_dynamic"));
        Canvas.changepreparedcharacter += () =>
        {

            spawnedCharacter = prefabmap[nowindex];
            nowindex++;
            if(nowindex >= Prefabs.count) nowindex = 0;

        };

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
