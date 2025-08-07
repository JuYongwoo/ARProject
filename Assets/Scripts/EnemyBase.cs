using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    static public Action<SceneManager.Sounds> playaudio;

    virtual protected void Start()
    {
        InGamePanel.EnemyCount++;
        playaudio(SceneManager.Sounds.spawn);
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
        playaudio(SceneManager.Sounds.destroy);
        InGamePanel.EnemyCount--;


    }


}
