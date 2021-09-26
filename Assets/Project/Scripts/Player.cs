using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed = 4f;
        public float jumpForce = 11f;
        public Vector3 groundDetectionOffset = new Vector3(0, -.05f, 0);
        public float groundDetectionRayDistance = .1f;

        private Rigidbody2D _rigidbody;
        private bool _isJumping;
        private Animator _animator;

        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsCrouching = Animator.StringToHash("isCrouching");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Jump();
            FlipPlayer();
        }

        private void FlipPlayer()
        {
            var x = _rigidbody.velocity.x;
            var localScale = transform.localScale;
            if ((x < 0 && localScale.x > 0) || (x > 0 && localScale.x < 0))
            {
                transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            }
        }

        private void Jump()
        {
            if (!Input.GetButtonDown("Jump") || !IsOnTheGround())
            {
                if (!_animator.GetBool(IsJumping) && !IsOnTheGround())
                    _animator.SetBool(IsJumping, true);
                
                return;
            }

            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _animator.SetBool(IsJumping, true);
            _isJumping = true;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private bool IsOnTheGround()
        {
            var adjustedOrigin = transform.position + groundDetectionOffset;
            var hit = Physics2D.Raycast(adjustedOrigin, Vector2.down, groundDetectionRayDistance);
            var isOnTheGround = hit.collider != null;
            return isOnTheGround;
        }


        private void Move()
        {
            var x = Input.GetAxis("Horizontal");
            var velocity = _rigidbody.velocity;
            if (x == 0f)
            {
                var decelerated = Mathf.Max(velocity.x - .5f, 0f);
                _rigidbody.velocity = new Vector2(decelerated, velocity.y);
                _animator.SetBool(IsWalking, false);
                if (_isJumping && IsOnTheGround())
                    _animator.SetBool(IsJumping, false);
                
                _animator.SetBool(IsCrouching, Input.GetAxis("Vertical") < 0);
                return;
            }

            if (!_animator.GetBool(IsCrouching))
            {
                var accelerated = x * moveSpeed;
                _rigidbody.velocity = new Vector2(accelerated, velocity.y);                
            }

            if (IsOnTheGround())
            {
                _animator.SetBool(IsJumping, false);
                _animator.SetBool(IsWalking, true);
                
            }
        }

        public void GameOver()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}