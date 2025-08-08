using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    static public Action<MainSceneObject.Sounds> playaudio;
    static public Action<int> enemyCountDelta;
    virtual protected void Start()
    {
        enemyCountDelta(1);
        playaudio(MainSceneObject.Sounds.spawn);
    }

    virtual protected void Update()
    {
        // ī�޶� �������� �̵� (���� ���� ���)
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;

        // ���� �ӵ��� �̵�
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += direction * 0.05f * Time.deltaTime;
    }
    private void OnDestroy()
    {
        playaudio(MainSceneObject.Sounds.destroy);
        enemyCountDelta(-1);


    }


}
