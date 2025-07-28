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
        buttonevent(); //�ʱ� 1ȸ ����
    }

    void Update()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (IsTouchOverUI(touch.position))
            return;

        // ȭ���� ��ġ���� �� ���� �����ϴ��� Ȯ��
        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                GameObject newprefab = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);

                Vector3 targetPos = Camera.main.transform.position;
                targetPos.y = newprefab.transform.position.y;
                newprefab.transform.LookAt(targetPos);

            }
        }
    }
    void initmap()
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

    bool IsTouchOverUI(Vector2 touchPos)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = touchPos;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        return raycastResults.Count > 0;
    }
    void assignfunc()
    {

        MainCanvas.changepreparedcharacter += buttonevent;
    }

    void buttonevent()
    {
        Array values = Enum.GetValues(typeof(Prefabs));
        nowindex = (Prefabs)(((int)nowindex + 1) % values.Length);

        characterPrefab = prefabmap[nowindex];
        changecharname(nowindex.ToString());
    }


}