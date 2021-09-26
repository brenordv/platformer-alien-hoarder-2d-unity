using UnityEngine;

namespace Project.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public delegate void EnemyHandler();

        public event EnemyHandler OnEnemyKill;

        public Vector3 targetPosition;
        public bool targetOnlyY;
        public bool flipSpriteYAxisOnWayBack;
        public float moveSpeed;
        public bool canMove = true;
        private Vector3 _startPosition;
        private bool _movingToTargetPos;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            _startPosition = transform.position;
            _movingToTargetPos = true;
            if (!targetOnlyY) return;
            targetPosition = new Vector3(_startPosition.x, targetPosition.y, _startPosition.z);
        }

        private void Update()
        {
            if (!canMove) return;
            Move();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            OnEnemyKill?.Invoke();
        }

        private void Move()
        {
            var currentPosition = transform.position;

            if (_movingToTargetPos && currentPosition == targetPosition)
            {
                _movingToTargetPos = false;
                if (flipSpriteYAxisOnWayBack)
                    _spriteRenderer.flipY = true;
            }
            else if (!_movingToTargetPos && currentPosition == _startPosition)
            {
                _movingToTargetPos = true;
                if (flipSpriteYAxisOnWayBack)
                    _spriteRenderer.flipY = false;
            }

            transform.position =
                Vector3.MoveTowards(
                    currentPosition,
                    _movingToTargetPos ? targetPosition : _startPosition,
                    moveSpeed * Time.deltaTime);
        }
    }
}