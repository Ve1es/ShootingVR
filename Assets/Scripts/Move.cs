using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f; // �������� �������� ������

    void Update()
    {
        // �������� ������� ������ �� ����������
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // ������������ ������ �������� �� ������ ������� ������ � ��������
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // ��������� �������� � ������� ������
        transform.Translate(movement);

        // ���� �� ������ ������������ ������, ��������������� ������ ���� � ����������� ���:
        // Rigidbody rb = GetComponent<Rigidbody>();
        // rb.MovePosition(rb.position + movement);
    }
}