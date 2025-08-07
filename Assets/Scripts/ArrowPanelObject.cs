using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowPanelObject : MonoBehaviour
{
    static public Func<List<GameObject>> getTargets; // 감지할 오브젝트 목록
    private Transform currenTarget; //현재 감지 목표 오브젝트
    private Camera arCamera;
    private Dictionary<ArrowObjects, GameObject> arraowObjectsmap = new Dictionary<ArrowObjects, GameObject>();

    enum ArrowObjects
    {
        LeftArrow,
        RightArrow,
        UpArrow,
        DownArrow
    }

    private void Awake()
    {
        initmapandmapping();

    }


    private void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        updateArrows();
    }

    void initmapandmapping()
    {
        arraowObjectsmap = new Dictionary<ArrowObjects, GameObject>();
        Transform[] childrens = GetComponentsInChildren<Transform>();
        foreach (ArrowObjects btn in Enum.GetValues(typeof(ArrowObjects)))
        {
            foreach (Transform t in childrens)
            {
                if (t.name == btn.ToString())
                {
                    arraowObjectsmap[btn] = t.gameObject;
                    break;
                }
            }
        }

    }

    void updateArrows()
    {

        // 모든 화살표 초기 비활성화 후
        arraowObjectsmap[ArrowObjects.LeftArrow].SetActive(false);
        arraowObjectsmap[ArrowObjects.RightArrow].SetActive(false);
        arraowObjectsmap[ArrowObjects.UpArrow].SetActive(false);
        arraowObjectsmap[ArrowObjects.DownArrow].SetActive(false);



        List<GameObject> targets = getTargets();
        if (getTargets().Count == 0) return;
        if (currenTarget == null)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null) continue;
                currenTarget = targets[i].transform; //가장 처음에 넣은 것을 currentTarget으로 설정한다.
            }
        }

        if (currenTarget == null) return; //위에서 찾아봤는데도 감지할 오브젝트가 없으면 리턴

        Vector3 toTarget = (currenTarget.position - arCamera.transform.position).normalized;
        Vector3 localDir = arCamera.transform.InverseTransformDirection(toTarget);

        // 수평 방향 판단 (Y축)
        if (Mathf.Abs(localDir.x) > Mathf.Abs(localDir.z))
        {
            if (localDir.x > 0)
                arraowObjectsmap[ArrowObjects.RightArrow].SetActive(true);
            else
                arraowObjectsmap[ArrowObjects.LeftArrow].SetActive(true);
        }

        // 수직 방향 판단 (X축)
        if (Mathf.Abs(localDir.y) > Mathf.Abs(localDir.z))
        {
            if (localDir.y > 0)
                arraowObjectsmap[ArrowObjects.UpArrow].SetActive(true);
            else
                arraowObjectsmap[ArrowObjects.DownArrow].SetActive(true);
        }
    }
}