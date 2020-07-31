using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    class SpawnEnemies : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _spawningAreas;
        [SerializeField]
        private GameObject[] _enemyPrefabs;
        [SerializeField]
        private float _spawningRate = 1f;
        public bool _spawningOn = false;

        private float _lastSpawn = 0;
        private int _enemyCount = 0;

        private int _selectedSpawnArea = 0;

        void Update()
        {
            // if something breaks in level 1 comment this out
            /*if(_spawningOn)
                SpawnEnemy(_selectedSpawnArea, _enemyAmount);*/
        }

        public void SpawnEnemy(int spawningArea, int enemyAmount)
        {
            var spawningAreaTransform = _spawningAreas[spawningArea].transform;

            // Calculate the area where object can be spawned. CAL: position -/+ (width / 2)
            var minX = spawningAreaTransform.localPosition.x - 
                         (spawningAreaTransform.localScale.x / 2);
            var maxX = spawningAreaTransform.localPosition.x +
                       (spawningAreaTransform.localScale.x / 2);
            var minZ = spawningAreaTransform.localPosition.z -
                       (spawningAreaTransform.localScale.z / 2);
            var maxZ = spawningAreaTransform.localPosition.z +
                       (spawningAreaTransform.localScale.z / 2);

            if (_enemyCount != enemyAmount)
            {
                if (Time.time - _lastSpawn >= _spawningRate)
                {
                    var ranNum = 0;
                    var position = transform.position + new Vector3
                                   (Random.Range(minX, maxX), _spawningAreas[spawningArea].transform.position.y - 5,
                                       Random.Range(minZ, maxZ));
                    Instantiate(_enemyPrefabs[ranNum], position, Quaternion.identity);
                    _lastSpawn = Time.time;

                    _enemyCount++;
                }
            }

            else
                return;
        }

        public void ChangeSelectedSpawnArea(int index)
        {
            _selectedSpawnArea = index;
        }
    }
}
