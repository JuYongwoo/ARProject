using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    static public Action<Sounds> playaudio;
    static public Action<int> enemyCountDelta;
    virtual protected void Start()
    {
        enemyCountDelta(1);
        playaudio(Sounds.Spawn);
    }

    virtual protected void Update()
    {
        // 카메라 방향으로 이동 (방향 벡터 계산)
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;

        // 일정 속도로 이동
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position += direction * 0.05f * Time.deltaTime;
    }
    private void OnDestroy()
    {
        playaudio(Sounds.Destroy);
        enemyCountDelta(-1);


    }


}
