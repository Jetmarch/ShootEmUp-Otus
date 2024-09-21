using System;
using UnityEngine;

namespace ShootEmUp
{
    public interface IGameListener
    {
        public static event Action<IGameListener> OnRegister;

        public static void Register(IGameListener listener)
        {
            OnRegister?.Invoke(listener);
        }
    }

    public interface IGameStartListener : IGameListener
    {
        void OnStart();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnFinish();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResume();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float delta);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float delta);
    }
}