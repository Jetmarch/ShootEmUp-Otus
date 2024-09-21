using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class BulletSystem : MonoBehaviour, IGameFixedUpdateListener
    {
        public struct Args
        {
            public Vector2 position;
            public Vector2 velocity;
            public Color color;
            public int physicsLayer;
            public int damage;
            public bool isPlayer;
        }

        [SerializeField] private GameObjectPool _bulletPool;

        [SerializeField] private BulletLevelBoundsChecker _levelBoundsChecker;

        [SerializeField] private BulletFactory _bulletFactory;

        private readonly HashSet<Bullet> _activeBullets = new();

        private void Start()
        {
            IGameListener.Register(this);
        }

        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletUtils.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (_activeBullets.Remove(bullet))
            {
                bullet.OnCollisionEntered -= OnBulletCollision;
                _bulletPool.ReturnObject(bullet.gameObject);
            }
        }

        public void CreateBullet(Args args)
        {
            var bullet = _bulletPool.GetObject().GetComponent<Bullet>();

            if(bullet == null)
            {
                throw new System.Exception("Bullet prefab doesn't contain Bullet script!");
#if UNITY_EDITOR
                EditorApplication.isPaused = true;
#endif
            }

            bullet = _bulletFactory.ConstructBullet(bullet, args);

            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }

        public void OnFixedUpdate(float delta)
        {
            _levelBoundsChecker.CheckLevelBounds(_activeBullets, RemoveBullet);
        }
    }
}