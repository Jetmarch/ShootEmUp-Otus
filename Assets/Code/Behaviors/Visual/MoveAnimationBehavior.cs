using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ZombieShooter.Behaviors.Visual
{
    [Serializable]
    public sealed class MoveAnimationBehavior : IEntityInit, IEntityUpdate
    {
        private ReactiveVariable<Vector3> _moveDirection;
        private Transform _visualTransform;
        private Animator _animator;
        private int _horizontalMoveDirection = Animator.StringToHash("Horizontal");
        private int _verticalMoveDirection = Animator.StringToHash("Vertical");
        
        public void Init(IEntity entity)
        {
            _visualTransform = entity.GetVisualTransform();
            _moveDirection = entity.GetMoveDirection();
            _animator = entity.GetAnimator();
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            var visualMoveDirection = _visualTransform.TransformDirection(_moveDirection.Value); 
            _animator.SetFloat(_verticalMoveDirection, visualMoveDirection.x);
            _animator.SetFloat(_horizontalMoveDirection, visualMoveDirection.z);
        }
    }
}