using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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

        // ȭ���� ��ġ���� �� ���� �����ϴ��� Ȯ��
        if (touch.phase == TouchPhase.Began)
        {
            if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                characterPrefab = Instantiate(characterPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
    void initmap()
    {
        prefabmap = new Dictionary<Prefabs, GameObject>();
        prefabmap.Add(Prefabs.dog, Resources.Load<GameObject>("Prefabs/DogPolyart"));
        prefabmap.Add(Prefabs.unitychan, Resources.Load<GameObject>("Prefabs/unitychan_dynamic"));
    }

    void assignfunc()
    {

        Canvas.changepreparedcharacter += buttonevent;
    }

    void buttonevent()
    {
        characterPrefab = prefabmap[nowindex];
        changecharname(Enum.GetName(typeof(Prefabs), nowindex));
        nowindex++;
        if (nowindex >= Prefabs.count) nowindex = 0;
    }

}
