using UnityEngine;

namespace Assets.Unity___Foundations_of_Audio.Scripts.System
{
    public class PlayerController : MonoBehaviour
    {


        [Header("Player")]
        [Tooltip("Move Speed Player")]
        public float moveSpeedPlayer = 4.0f;
        [Tooltip("Sprint speed of the character")]
        public float SprintSpeed = 6.0f;
        [Tooltip("Rotation speed of the character")]
        public float RotationSpeed = 1.0f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;
        [Range(0.0f, 0.7f)]
        public float RotationSmoothTime = 0.12f;

        [Header("Cinemachine")]
        public GameObject CinemachineCameraTarget;
        [Tooltip("Move camera up")]
        public float TopClamp = 90.0f;
        [Tooltip("Move camera Down")]
        public float BottomClamp = -90.0f;

        private int _animationPlayerHash = Animator.StringToHash("Input");
        private int _animationPlayerJumpHash = Animator.StringToHash("Jump");

        private CharacterController characterController;
        private Animator animator;
        private Vector3 playerCharacterMove;
        private Vector3 moveCamera;
        private GameObject _mainCamera;
        private Rigidbody _rigidbody;

        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _threshold = 0.01f;
        private float _cinemachineTargetPitch;

        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            MovePlayer();
            MoveCameraRotation();
        }

        private void MovePlayer()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            float jumpPlayer = Input.GetAxis("X");

            playerCharacterMove = new Vector3(moveHorizontal, 0.0f, moveVertical);

            if (playerCharacterMove != Vector3.zero)
            {
                if (moveHorizontal != 0.0f || moveVertical != 0.0f)
                {
                    float moveSpeedMultiplier = moveHorizontal > 0.0f || moveVertical > 0.0f ? 1.0f : 3.0f;
                    animator.SetFloat(_animationPlayerHash, playerCharacterMove.sqrMagnitude + moveSpeedMultiplier);
                    _rigidbody.velocity = transform.forward * moveSpeedPlayer * moveSpeedMultiplier;

                    if(jumpPlayer > 0)
                    {
                        animator.SetBool(_animationPlayerJumpHash, true);
                    }
                    else
                    {
                        animator.SetBool(_animationPlayerJumpHash, false);
                    }
                }
                else if (moveHorizontal < 0.0f || moveVertical < 0.0f)
                {
                    animator.SetFloat(_animationPlayerHash, playerCharacterMove.sqrMagnitude + 3);
                    _rigidbody.velocity = -transform.forward * moveSpeedPlayer;
                }

                //Input  directions of character with base in rotation and used calculate of Euler
                Vector3 inputDirection = new Vector3(playerCharacterMove.x, 0.0f, playerCharacterMove.y).normalized;

                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

                characterController.Move(targetDirection.normalized * (moveSpeedPlayer * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
            }
            else
            {
                animator.SetFloat(_animationPlayerHash, 0);
            }
        }

        private void MoveCameraRotation()
        {

            float moveRVertical = Input.GetAxis("Vertical");
            float moveRHorizontal = Input.GetAxis("Horizontal");

            Debug.Log("Rotation" + moveCamera.magnitude);

            moveCamera = new Vector3(moveRHorizontal, 0.0f, moveRVertical);

            if (moveCamera.magnitude >= _threshold)
            {
                float deltaTimeMultiplier = moveRHorizontal > 0.0f || moveRVertical > 0 ? 1.0f : Time.deltaTime;

                _cinemachineTargetPitch += moveCamera.y * RotationSpeed * deltaTimeMultiplier;
                _rotationVelocity = moveCamera.x * RotationSpeed * deltaTimeMultiplier;

                // clamp our pitch rotation
                _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

                // Update Cinemachine camera target pitch
                CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

                // rotate the player left and right
                transform.Rotate(Vector3.up * _rotationVelocity);
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

    }
}
