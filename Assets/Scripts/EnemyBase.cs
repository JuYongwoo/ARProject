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
        // ī�޶� �������� �̵� (���� ���� ���)
        Vector3 direction = (Camera.main.transform.position - transform.position).normalized;

        // ���� �ӵ��� �̵�
        transform.position += direction * 0.05f * Time.deltaTime;
    }
}
