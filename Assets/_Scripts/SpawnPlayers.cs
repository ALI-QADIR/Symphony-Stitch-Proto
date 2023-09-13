using Photon.Pun;
using UnityEngine;

namespace Assets._Scripts
{
    public class SpawnPlayers : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _minX = -5f, _minZ = -5f, _maxX = 5f, _maxY = 5f;

        private void Start()
        {
            var randomX = Random.Range(_minX, _maxX);
            var randomZ = Random.Range(_minZ, _maxY);
            PhotonNetwork.Instantiate(_playerPrefab.name, new Vector3(randomX, 0.5f, randomZ), Quaternion.identity);
        }
    }
}