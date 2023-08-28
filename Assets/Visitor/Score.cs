using UnityEngine;

namespace Assets.Visitor
{
    public class Score
    {
        public int Value => _enemyVisiter.Score;

        private IEnemyDeathNotifier _enemyDeathNotifier;

        private EnemyVisiter _enemyVisiter;

        public Score(IEnemyDeathNotifier enemyDeathNotifier)
        {
            _enemyDeathNotifier = enemyDeathNotifier;
            _enemyDeathNotifier.Notified += OnEnemyKilled;

            _enemyVisiter = new EnemyVisiter();
        }

        ~Score() => _enemyDeathNotifier.Notified -= OnEnemyKilled;

        public void OnEnemyKilled(Enemy enemy)
        {
            _enemyVisiter.Visit(enemy);
            Debug.Log($"Счет: {Value}");
        }

        private class EnemyVisiter : IEnemyVisitor
        {
            public int Score { get; private set; }

            public void Visit(Ork ork) => Score += 20;

            public void Visit(Human human) => Score += 5;

            public void Visit(Elf elf) => Score += 10;

            public void Visit(Enemy enemy) => Visit((dynamic)enemy);

            public void Visit(Vampire vampire) => Score += 25;
        }
    }
}
