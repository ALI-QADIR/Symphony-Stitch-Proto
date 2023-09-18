using UnityEngine;
using Photon.Pun;

namespace Assets._Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private GameObject _enemyPrefab;

        [SerializeField] private float _timeBetweenSpawns = 5f;
        private float _elapsedTime = 0f;
        

        private void SpawnEnemy()
        {
            var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            PhotonNetwork.Instantiate(_enemyPrefab.name, spawnPoint.position, Quaternion.identity);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!PhotonNetwork.IsMasterClient || PhotonNetwork.CurrentRoom.PlayerCount < 2) return;

            _elapsedTime += Time.deltaTime;
            if (!(_elapsedTime >= _timeBetweenSpawns)) return;
            _elapsedTime = 0f;
            SpawnEnemy();
        }
    }
}
