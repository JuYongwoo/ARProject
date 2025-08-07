using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    virtual protected void Start()
    {
        
    }

    virtual protected void Update()
    {
        // 카메라 방향으로 이동 (방향 벡터 계산)
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;

        // 일정 속도로 이동
        transform.position += direction * 0.05f * Time.deltaTime;
    }
}
