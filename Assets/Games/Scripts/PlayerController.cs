using UnityEngine;

namespace Assets.Unity___Foundations_of_Audio.Scripts.System
{
    public class PlayerController : MonoBehaviour
    {

        private CharacterController characterController;
        private Animator animator;
        private Vector3 playerCharacterMove;


        private float moveSpeedPlayer = 0.5f;
        float moveSpeedCamera = 50.0f;
        private int inputXHas = Animator.StringToHash("inputX");
        private int inputYHas = Animator.StringToHash("inputY");

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {

            float moveHorizontal = Input.GetAxis("L_Horizontal");
            float moveVertical = Input.GetAxis("L_Vertical");

            playerCharacterMove = new Vector3(moveHorizontal, moveVertical);

            if (Mathf.Abs(moveHorizontal) > 0.01f || Mathf.Abs(moveVertical) > 0.01f)
            {
                animator.SetFloat(inputXHas, playerCharacterMove.x);
                animator.SetFloat(inputYHas, playerCharacterMove.y);

                Vector3 movement = new Vector3(moveHorizontal, -9.81f, moveVertical);

                characterController.Move(movement * moveSpeedPlayer * Time.deltaTime);
            }

            MovePlayerVertical();

        }

        private void MovePlayerVertical()
        {

            // Obtém a posição do analógico direito (R3)
            float moveRVertical = Input.GetAxis("R_Vertical");

            Vector3 movementCamera = new Vector3(0.0f, moveRVertical, 0.0f);

            transform.Rotate(movementCamera * moveSpeedCamera * Time.deltaTime);
        }

    }
}
