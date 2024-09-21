using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class Bullet : MonoBehaviour, IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public bool isPlayer;

        [NonSerialized] public int damage;

        [SerializeField] private Rigidbody2D _rb2D;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector2 _velocity;

        private void Start()
        {
            IGameListener.Register(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
            _rb2D.velocity = velocity;
        }

        public void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void OnPause()
        {
            _rb2D.velocity = Vector2.zero;
        }

        public void OnResume()
        {
            _rb2D.velocity = _velocity;
        }

        public void OnFinish()
        {
            _rb2D.velocity = Vector2.zero;
        }
    }
}