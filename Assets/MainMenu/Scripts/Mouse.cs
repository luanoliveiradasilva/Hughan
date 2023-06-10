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
        

        // Calcular a dire��o do movimento
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        movement = movement.normalized;

        // Calcular a posi��o atual do tri�ngulo
        Vector3 currentPosition = transform.position;

        // Calcular a nova posi��o do tri�ngulo com base na velocidade e no tempo
        Vector3 newPosition = currentPosition + movement * cursorSpeed * Time.deltaTime;

        // Atualizar a posi��o do tri�ngulo
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
