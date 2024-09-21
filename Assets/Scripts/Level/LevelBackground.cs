using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class LevelBackground : MonoBehaviour, IGameFixedUpdateListener
    {
        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private float _positionX;

        private float _positionZ;

        private Transform _myTransform;

        [SerializeField] private Params _params;

        public void OnFixedUpdate(float delta)
        {
            MoveBackground(delta);
        }

        private void Awake()
        {
            _startPositionY = _params.StartPositionY;
            _endPositionY = _params.EndPositionY;
            _movingSpeedY = _params.MovingSpeedY;
            _myTransform = transform;
            var position = _myTransform.position;
            _positionX = position.x;
            _positionZ = position.z;
        }

        private void Start()
        {
            IGameListener.Register(this);
        }

        private void MoveBackground(float delta)
        {
            if (_myTransform.position.y <= _endPositionY)
            {
                _myTransform.position = new Vector3(
                    _positionX,
                    _startPositionY,
                    _positionZ
                );
            }

            _myTransform.position -= new Vector3(
            _positionX,
            _movingSpeedY * delta,
            _positionZ
            );
        }

        [Serializable]
        public sealed class Params
        {
            [SerializeField]
            public float StartPositionY;

            [SerializeField]
            public float EndPositionY;

            [SerializeField]
            public float MovingSpeedY;
        }
    }
}