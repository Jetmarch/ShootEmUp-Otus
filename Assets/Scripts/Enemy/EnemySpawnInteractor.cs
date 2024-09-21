using System.Collections;
using UnityEngine;

namespace ShootEmUp
{
    public class EnemySpawnInteractor : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private float _spawnDelay = 1f;
        
        [SerializeField] private EnemyManager _enemyManager;

        private void Start()
        {
            IGameListener.Register(this);
        }

        public void OnStart()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);
                _enemyManager.CreateEnemy();
            }
        }
    }
}