using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARCharacterSpawner : MonoBehaviour
{

    enum Prefabs : int
    {
        dog,
        unitychan,
        tentacle
    }


    private GameObject characterPrefab;

    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private Dictionary<Prefabs, GameObject> prefabmap;
    private Prefabs nowindex = Prefabs.dog;

    static public Action<string> changecharname;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();

        initmap();
        assignfunc();
    }

    private void Start()
    {
        buttonevent(); //초기 1회 실행
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;

        // 화면을 터치했을 때 평면과 교차하는지 확인
        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                characterPrefab = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);

                Vector3 targetPos = Camera.main.transform.position;
                targetPos.y = characterPrefab.transform.position.y;
                characterPrefab.transform.LookAt(targetPos);

            }
        }
    }
    void initmap()
    {
        prefabmap = new Dictionary<Prefabs, GameObject>();

        foreach (Prefabs key in Enum.GetValues(typeof(Prefabs)))
        {

            string path = "Prefabs/"+key;
            GameObject prefab = Resources.Load<GameObject>(path);

            if (prefab != null)
            {
                prefabmap.Add(key, prefab);
            }
        }
    }


    void assignfunc()
    {

        TipCanvas.changepreparedcharacter += buttonevent;
    }

    void buttonevent()
    {
        Array values = Enum.GetValues(typeof(Prefabs));
        nowindex = (Prefabs)(((int)nowindex + 1) % values.Length);

        characterPrefab = prefabmap[nowindex];
        changecharname(nowindex.ToString());
    }


}
