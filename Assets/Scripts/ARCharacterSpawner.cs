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


    [Header("캐릭터 프리팹")]
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
        // 캐릭터 이미 소환되었으면 무시
        if (spawnedCharacter != null)
            return;

        // 터치 감지
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        // 화면을 터치했을 때 평면과 교차하는지 확인
        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                // 캐릭터 소환
                spawnedCharacter = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }

}
