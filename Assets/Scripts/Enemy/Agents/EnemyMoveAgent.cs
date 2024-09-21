using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class EnemyMoveAgent : MonoBehaviour, IGameFixedUpdateListener
    {
        public bool IsReached
        {
            get { return _isReached; }
        }

        public event Action OnReachedDestination;

        [SerializeField] private MoveComponent _moveComponent;

        [SerializeField] private float _minDestinationMagnitude = 0.25f;

        private Vector2 _destination;

        private bool _isReached;

        private void Start()
        {
            IGameListener.Register(this);
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        public void OnFixedUpdate(float delta)
        {
            if (_isReached)
            {
                return;
            }

            Move(delta);
        }

        private void Move(float delta)
        {
            var vector = _destination - (Vector2)transform.position;
            if (vector.magnitude <= _minDestinationMagnitude)
            {
                _isReached = true;
                OnReachedDestination?.Invoke();
                return;
            }

            var direction = vector.normalized * delta;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}