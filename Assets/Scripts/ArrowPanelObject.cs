using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ArrowPanelObject : MonoBehaviour
{
    static public Func<List<GameObject>> getTargets; // ������ ������Ʈ ���
    private Transform currenTarget; //���� ���� ��ǥ ������Ʈ
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

        // ��� ȭ��ǥ �ʱ� ��Ȱ��ȭ ��
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
                currenTarget = targets[i].transform; //���� ó���� ���� ���� currentTarget���� �����Ѵ�.
            }
        }

        if (currenTarget == null) return; //������ ã�ƺôµ��� ������ ������Ʈ�� ������ ����

        Vector3 toTarget = (currenTarget.position - arCamera.transform.position).normalized;
        Vector3 localDir = arCamera.transform.InverseTransformDirection(toTarget);

        // ���� ���� �Ǵ� (Y��)
        if (Mathf.Abs(localDir.x) > Mathf.Abs(localDir.z))
        {
            if (localDir.x > 0)
                arraowObjectsmap[ArrowObjects.RightArrow].SetActive(true);
            else
                arraowObjectsmap[ArrowObjects.LeftArrow].SetActive(true);
        }

        // ���� ���� �Ǵ� (X��)
        if (Mathf.Abs(localDir.y) > Mathf.Abs(localDir.z))
        {
            if (localDir.y > 0)
                arraowObjectsmap[ArrowObjects.UpArrow].SetActive(true);
            else
                arraowObjectsmap[ArrowObjects.DownArrow].SetActive(true);
        }
    }
}