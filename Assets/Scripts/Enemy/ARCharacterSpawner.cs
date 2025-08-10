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


    /*
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
*/

    private Dictionary<Prefabs, GameObject> prefabmap;
    private Prefabs currentindex = Prefabs.dog;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    static public Action<string> changecharname;

    void Awake()
    {
        //raycastManager = GetComponent<ARRaycastManager>();

        prefabmap = Util.mapDictionaryWithEnumAndLoad<Prefabs, GameObject>("Prefabs");
        assignfunc();
        ArrowPanelObject.getTargets = () => spawnedObjects;
    }

    private void Start()
    {
        buttonevent(); //�ʱ� 1ȸ ����
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
        // ȭ���� ��ġ���� �� ���� �����ϴ��� Ȯ��
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
        //changecharname(currentindex.ToString()); //MainPanel�� �ؽ�Ʈ�� �ٲ۴�.
    }

    private void spawnRandomCharacter() // dog, unitychan, tentacle �� �ϳ��� �������� ���� // ����� dog�� ������
    {

        Vector3 randomDir = UnityEngine.Random.onUnitSphere; // ���� 1, ��� ���� ����

        Vector3 spawnPosition = Camera.main.transform.position + randomDir * 1f;

        GameObject go = Instantiate(prefabmap[Prefabs.dog], spawnPosition, Quaternion.identity);

        spawnedObjects.Add(go);
    }
}