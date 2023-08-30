using System;
using UnityEngine;

namespace Assets.Visitor
{
    public class EnemyFactory: MonoBehaviour
    {
        [SerializeField] private Human _humanPrefab;
        [SerializeField] private Ork _orkPrefab;
        [SerializeField] private Elf _elfPrefab;
        [SerializeField] private Vampire _vampirePrefab;

        public Enemy Get(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Elf:
                    Enemy elf = _elfPrefab;
                    return elf;

                case EnemyType.Human:
                    Enemy human = _humanPrefab;
                    return human;

                case EnemyType.Ork:
                    Enemy ork = _orkPrefab;
                    return ork;

                case EnemyType.Vampire:
                    Enemy vamprie = _vampirePrefab;
                    return vamprie;

                default:
                    throw new ArgumentException(nameof(type));
            }
        }
    }
}
