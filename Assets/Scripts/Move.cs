using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f; // Скорость движения игрока

    void Update()
    {
        // Получаем входные данные от клавиатуры
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Рассчитываем вектор движения на основе входных данных и скорости
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;

        // Применяем движение к позиции игрока
        transform.Translate(movement);

        // Если вы хотите использовать физику, закомментируйте строку выше и используйте эту:
        // Rigidbody rb = GetComponent<Rigidbody>();
        // rb.MovePosition(rb.position + movement);
    }
}
