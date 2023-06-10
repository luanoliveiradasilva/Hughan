using UnityEngine;
using UnityEngine.UI;
using Assets.Shared.Scripts;

public class Mouse : MonoBehaviour
{
    [Header("Mouse")]
    [Tooltip("Velocidade do mouse")]
    [SerializeField] private float cursorSpeed = 8f;

    [Header("Buttons")]
    [SerializeField] Button startButton;

    private static LoadGamesScenes _loadGamesScenes;


    private void Start()
    {
      startButton.onClick.AddListener(_loadGamesScenes.loadScene);
    }

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        float jumpPlayer = Input.GetAxis("X");

        if (collision.CompareTag("Menu"))
        {
            if(jumpPlayer > 0)
            {
                Debug.Log("Debug " + jumpPlayer);
               
            }
        }
    }
}
