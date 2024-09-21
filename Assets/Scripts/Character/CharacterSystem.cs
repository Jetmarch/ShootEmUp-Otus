using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterSystem : MonoBehaviour, IGameFixedUpdateListener
    {
        public Action<BulletSystem.Args> OnRequestBullet;

        [SerializeField] private GameObject _character;

        [SerializeField] private BulletConfig _bulletConfig;

        [SerializeField] private MoveComponent _moveComponent;

        [SerializeField] private HitPointsComponent _hitPointsComponent;

        private float _horizontalDirection;

        private bool _fireRequired;

        private void Start()
        {
            IGameListener.Register(this);
        }

        private void ShootBullet()
        {
            var weapon = _character.GetComponent<WeaponComponent>();

            OnRequestBullet?.Invoke(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int)_bulletConfig.PhysicsLayer,
                color = _bulletConfig.Color,
                damage = _bulletConfig.Damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }

        public void Fire()
        {
            _fireRequired = true;
        }

        public void MoveRight()
        {
            _horizontalDirection = 1;
        }

        public void MoveLeft()
        {
            _horizontalDirection = -1;
        }

        public void StopMove()
        {
            _horizontalDirection = 0;
        }

        public void OnFixedUpdate(float delta)
        {
            if (_fireRequired)
            {
                ShootBullet();

                _fireRequired = false;
            }

            _moveComponent.MoveByRigidbodyVelocity(new Vector2(_horizontalDirection, 0) * delta);
        }
    }
}