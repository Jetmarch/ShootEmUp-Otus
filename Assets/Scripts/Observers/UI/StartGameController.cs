using ShootEmUp.UI;
using UnityEngine;

namespace ShootEmUp
{
    public class StartGameController : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private ButtonView _startGameView;

        [SerializeField] private ButtonView _gameView;

        [SerializeField] private StartGameTimer _startGameTimer;

        private void Start()
        {
            IGameListener.Register(this);
        }

        /// <summary>
        /// ����������� OnEnable, ��� ��� StartGameController ��������� ������� ����
        /// </summary>
        private void OnEnable()
        {
            _startGameView.OnButtonClick += _startGameTimer.StartGame;
            _startGameTimer.OnTimerUpdate += UpdateStartGameTimerText;
        }

        /// <summary>
        /// ����������� OnDisable, ��� ��� StartGameController ��������� ������� ����
        /// </summary>
        private void OnDisable()
        {
            _startGameView.OnButtonClick -= _startGameTimer.StartGame;
            _startGameTimer.OnTimerUpdate -= UpdateStartGameTimerText;
        }

        private void UpdateStartGameTimerText(int time)
        {
            _startGameView.UpdateText(time.ToString());
        }

        public void OnStart()
        {
            _startGameView.Hide();
            _gameView.Show();
        }
    }
}