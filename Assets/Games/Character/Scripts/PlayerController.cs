using System.Collections;
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
        private int _animationPlayerAttackHas = Animator.StringToHash("Attack");

        private CharacterController _characterController;
        private Animator _animator;
        private Vector3 playerCharacterMove;
        private Vector3 moveCamera;
        private GameObject _mainCamera;
        private Rigidbody _rigidbody;
        private AnimatorClipInfo[] _CurrentClipInfo;
        [SerializeField] private GameObject helthBar;

        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _cinemachineTargetY;
        private float _cinemachineTargetX;

        public float CameraAngleOverride = 0.0f;
        public bool LockCameraPosition = false;

        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }

            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();

        }

        private void Start()
        {
            helthBar.SetActive(true);
        }

        private void Update()
        {
            MovePlayer();
        }

        private void LateUpdate()
        {
            MoveCameraRotation();
        }
        private void MovePlayer()
        {
            float moveVertical = Input.GetAxis("Vertical");
            float moveHorizontal = Input.GetAxis("Horizontal");
            float jumpPlayer = Input.GetAxis("X");
            float attackControl = Input.GetAxis("L1");
            bool jumpKeyboard = Input.GetKey(KeyCode.E);
            bool attackKeyboard = Input.GetKey(KeyCode.Q);

            playerCharacterMove = new Vector3(moveHorizontal, moveVertical);

            if (playerCharacterMove.sqrMagnitude >= 0.01f)
            {

                float moveSpeedMultiplier = (Mathf.Abs(moveVertical) > 0.0f) ? 1.0f : 3.0f;

                _animator.SetFloat(_animationPlayerHash, playerCharacterMove.sqrMagnitude + moveSpeedMultiplier);
                _rigidbody.velocity = transform.forward * moveSpeedPlayer * moveSpeedMultiplier;

                if (jumpPlayer > 0 || jumpKeyboard == true)
                    StartCoroutine(Jump());

                if (attackControl > 0 || attackKeyboard == true)
                    StartCoroutine(Attacks());

                //Input  directions of character with base in rotation and used calculate of Euler
                Vector3 inputDirection = new Vector3(playerCharacterMove.x, 0.0f, playerCharacterMove.y).normalized;

                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);

                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;                

                _characterController.Move(targetDirection.normalized * (moveSpeedPlayer * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
            }
            else
            {
                _animator.SetFloat(_animationPlayerHash, 0);

                if (jumpPlayer > 0 || jumpKeyboard == true)
                    StartCoroutine(Jump());

                if (attackControl > 0 || attackKeyboard == true)
                    StartCoroutine(Attacks());
            }
        }
        private void MoveCameraRotation()
        {

            float moveRVertical = Input.GetAxis("R_Vertical");
            float moveRHorizontal = Input.GetAxis("R_Horizontal");

            moveCamera = new Vector3(moveRHorizontal, moveRVertical, 0.0f);

            if (moveCamera.sqrMagnitude >= 0.01f)
            {
                float deltaTimeMultiplier = (Mathf.Abs(moveRHorizontal) > 0.0f || Mathf.Abs(moveRVertical) > 0.0f) ? 1.0f : Time.deltaTime;
                _cinemachineTargetX += moveCamera.x * deltaTimeMultiplier;
                _cinemachineTargetY += moveCamera.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetX = ClampAngle(_cinemachineTargetX, float.MinValue, float.MaxValue);
            _cinemachineTargetY = ClampAngle(_cinemachineTargetY, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetY + CameraAngleOverride,
                _cinemachineTargetX, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private IEnumerator Attacks()
        {
            _CurrentClipInfo = this._animator.GetCurrentAnimatorClipInfo(0);

            _animator.SetBool(_animationPlayerAttackHas, true);
            yield return new WaitForSeconds(_CurrentClipInfo[0].clip.length);
            _animator.SetBool(_animationPlayerAttackHas, false);       

        }

        private IEnumerator Jump()
        {
            _CurrentClipInfo = this._animator.GetCurrentAnimatorClipInfo(0);

            _animator.SetBool(_animationPlayerJumpHash, true);
            yield return new WaitForSeconds(_CurrentClipInfo[0].clip.length);
            _animator.SetBool(_animationPlayerJumpHash, false);
        }

    }
}
