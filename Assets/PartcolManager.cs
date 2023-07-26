using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartcolManager : MonoBehaviour
{
    public float bloodSped;
    [SerializeField] private Transform sped;


    private void Start()
    {
        // �������� ������� ������� �������
        Vector3 currentPosition = transform.position;

        // ���������� ��������� �������� ��� �������� �� ���� X � Z
        float randomOffsetX = Random.Range(-1f, 1f);
        float randomOffsetZ = Random.Range(-1f, 1f);

        // ��������� ������� ������� � ������ ��������� ��������
        transform.position = new Vector3(currentPosition.x + randomOffsetX, currentPosition.y, currentPosition.z + randomOffsetZ);
    }

    void Update()
    {
            bloodSped = 10;
        
        sped.Translate(sped.forward * Time.deltaTime * bloodSped);
    }
}
