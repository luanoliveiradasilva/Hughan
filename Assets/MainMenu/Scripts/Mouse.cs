using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    [SerializeField] private float cursorSpeed = 8f;

    private void Update()
    {
        // Obter a entrada do teclado nos eixos horizontal e vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calcular a direção do movimento
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        movement = movement.normalized;

        // Calcular a posição atual do triângulo
        Vector3 currentPosition = transform.position;

        // Calcular a nova posição do triângulo com base na velocidade e no tempo
        Vector3 newPosition = currentPosition + movement * cursorSpeed * Time.deltaTime;

        // Atualizar a posição do triângulo
        transform.position = newPosition;
    }
}
