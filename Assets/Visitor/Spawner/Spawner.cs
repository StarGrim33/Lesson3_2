using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Visitor
{
    public class Spawner: MonoBehaviour, IEnemyDeathNotifier
    {
        [SerializeField] private float _spawnCooldown;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private float _maxTotalWeight;

        private List<Enemy> _spawnedEnemies  = new();
        private Coroutine _spawn;
        private bool _isSpawning = false;
        private float _currentTotalWeight;

        public event Action<Enemy> Notified;

        private void Start()
        {
            StartWork();
        }

        public void StartWork()
        {
            StopWork();

            _isSpawning = true;
            _spawn = StartCoroutine(Spawn());
        }

        public void StopWork()
        {
            if (_spawn != null)
                StopCoroutine(_spawn);

            _isSpawning = false;
        }

        public void KillRandomEnemy()
        {
            if(_spawnedEnemies.Count < 0)
                return;

            _spawnedEnemies[UnityEngine.Random.Range(0, _spawnedEnemies.Count)].Die();
        }

        private IEnumerator Spawn()
        {
            while (_isSpawning)
            {
                EnemyType randomType = (EnemyType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(EnemyType)).Length);
                Enemy enemy = _enemyFactory.Get(randomType);

                if (enemy is IWeighted weightedEnemy && _currentTotalWeight + weightedEnemy.Weight <= _maxTotalWeight)
                {
                    enemy.MoveTo(_spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position);
                    enemy.Died += OnEnemyDied;
                    _spawnedEnemies.Add(enemy);
                    _currentTotalWeight += weightedEnemy.Weight;
                    Instantiate(enemy);
                    Debug.Log($"Current weight is {_currentTotalWeight}");
                }

                yield return new WaitForSeconds(_spawnCooldown);
            }
        }

        private void OnEnemyDied(Enemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            Notified.Invoke(enemy);
            _spawnedEnemies.Remove(enemy);
        }
    }
}
