using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager : MonoBehaviour
    {
        private List<IGameListener> _gameListeners = new();
        private List<IGameStartListener> _gameStartListeners = new();
        private List<IGameFinishListener> _gameFinishListeners = new();
        private List<IGamePauseListener> _gamePauseListeners = new();
        private List<IGameResumeListener> _gameResumeListeners = new();
        private List<IGameUpdateListener> _gameUpdateListeners = new();
        private List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

        private GameState _state;

        private void Awake()
        {
            IGameListener.OnRegister += RegisterListener;
        }

        private void RegisterListener(IGameListener obj)
        {
            _gameListeners.Add(obj);


            if(obj is IGameStartListener startListener)
            {
                _gameStartListeners.Add(startListener);
            }

            if (obj is IGameFinishListener finishListener)
            {
                _gameFinishListeners.Add(finishListener);
            }

            if (obj is IGamePauseListener pauseListener)
            {
                _gamePauseListeners.Add(pauseListener);
            }

            if (obj is IGameResumeListener resumeListener)
            {
                _gameResumeListeners.Add(resumeListener);
            }

            if (obj is IGameUpdateListener updateListener)
            {
                _gameUpdateListeners.Add(updateListener);
            }

            if (obj is IGameFixedUpdateListener fixedUpdateListener)
            {
                _gameFixedUpdateListeners.Add(fixedUpdateListener);
            }
        }

        private void RemoveListener(IGameListener obj)
        {
            _gameListeners.Remove(obj);


            if (obj is IGameStartListener startListener)
            {
                _gameStartListeners.Remove(startListener);
            }

            if (obj is IGameFinishListener finishListener)
            {
                _gameFinishListeners.Remove(finishListener);
            }

            if (obj is IGamePauseListener pauseListener)
            {
                _gamePauseListeners.Remove(pauseListener);
            }

            if (obj is IGameResumeListener resumeListener)
            {
                _gameResumeListeners.Remove(resumeListener);
            }

            if (obj is IGameUpdateListener updateListener)
            {
                _gameUpdateListeners.Remove(updateListener);
            }

            if (obj is IGameFixedUpdateListener fixedUpdateListener)
            {
                _gameFixedUpdateListeners.Remove(fixedUpdateListener);
            }
        }

        private void Update()
        {
            if (_state != GameState.Running) return;

            var delta = Time.deltaTime;
            foreach(var listener in _gameUpdateListeners)
            {
                listener.OnUpdate(delta);
            }
        }

        private void FixedUpdate()
        {
            if (_state != GameState.Running) return;

            var delta = Time.fixedDeltaTime;
            foreach (var listener in _gameFixedUpdateListeners)
            {
                listener.OnFixedUpdate(delta);
            }
        }

        [ContextMenu("Start game")]
        public void StartGame()
        {
            _state = GameState.Starting;
            foreach (var listener in _gameStartListeners)
            {
                listener.OnStart();
            }
            _state = GameState.Running;
        }

        [ContextMenu("Finish game")]
        public void FinishGame()
        {
            _state = GameState.Finished;
            foreach (var listener in _gameFinishListeners)
            {
                listener.OnFinish();
            }
        }

        [ContextMenu("Pause game")]
        public void PauseGame()
        {
            _state = GameState.Paused;
            foreach (var listener in _gamePauseListeners)
            {
                listener.OnPause();
            }
        }

        [ContextMenu("Resume game")]
        public void ResumeGame()
        {
            _state = GameState.Running;
            foreach (var listener in _gameResumeListeners)
            {
                listener.OnResume();
            }
        }
    }
}