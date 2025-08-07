using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.GraphicsBuffer;

public class ARCharacterSpawner : MonoBehaviour
{

    enum Prefabs : int
    {
        dog,
        unitychan,
        tentacle
    }



    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private Dictionary<Prefabs, GameObject> prefabmap;
    private Prefabs currentindex = Prefabs.dog;

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
        InvokeRepeating(nameof(spawnRandomCharacter), 3f, 10f);
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (IsTouchOverUI(touch.position))
            return;


        /*
        // 화면을 터치했을 때 평면과 교차하는지 확인
        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                GameObject newprefab = Instantiate(prefabmap[nowindex], hitPose.position, hitPose.rotation);

                Vector3 targetPos = Camera.main.transform.position;
                targetPos.y = newprefab.transform.position.y;
                newprefab.transform.LookAt(targetPos);

            }
        }
        */

    }
    private void initmap()
    {
        prefabmap = new Dictionary<Prefabs, GameObject>();

        foreach (Prefabs key in Enum.GetValues(typeof(Prefabs)))
        {

            string path = "Prefabs/" + key;
            GameObject prefab = Resources.Load<GameObject>(path);

            if (prefab != null)
            {
                prefabmap.Add(key, prefab);
            }
        }
    }

    private bool IsTouchOverUI(Vector2 touchPos)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = touchPos;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        return raycastResults.Count > 0;
    }
    private void assignfunc()
    {

        MainPanelObject.changepreparedcharacter += buttonevent;
    }

    private void buttonevent()
    {
        Array values = Enum.GetValues(typeof(Prefabs));
        currentindex = (Prefabs)(((int)currentindex + 1) % values.Length);
        changecharname(currentindex.ToString());
    }

    private void spawnRandomCharacter()
    {

        Vector3 randomDir = UnityEngine.Random.onUnitSphere; // 길이 1, 모든 방향 포함

        Vector3 spawnPosition = Camera.main.transform.position + randomDir * 3f;

        GameObject go = Instantiate(prefabmap[currentindex], spawnPosition, Quaternion.identity);

        ArrowPanelObject.targets.Add(go.transform);
    }
}