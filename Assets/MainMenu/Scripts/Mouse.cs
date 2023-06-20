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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f);
        movement = movement.normalized;

        Vector3 currentPosition = transform.position;

        Vector3 newPosition = currentPosition + movement * cursorSpeed * Time.deltaTime;

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
