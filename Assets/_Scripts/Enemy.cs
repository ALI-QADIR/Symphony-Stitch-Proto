using UnityEngine;
using Photon.Pun;

namespace Assets._Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class Enemy : MonoBehaviour
    {
        private PlayerController[] _players;
        private PlayerController _target;
        private CharacterController _controller;
        private bool _hasDeductedHealth;
        private EnemySpawner _enemySpawner;

        [SerializeField] private float _speed = 5f;

        private void Start()
        {
            _hasDeductedHealth = false;
            _controller = GetComponent<CharacterController>();
            _players = FindObjectsOfType<PlayerController>();
            _enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        }

        private void Update()
        {
            // if there are less than 2 players in the room, return
            if (!Health.isGameActive) return;
            Move();
            CheckHit();
        }

        private void CheckHit()
        {
            var hits = Physics.OverlapSphere(transform.position, 0.55f);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Ray"))
                {
                    _enemySpawner.DestroyEnemy(this);
                }
                else if (hit.CompareTag("Player") && !_hasDeductedHealth)
                {
                    var player = hit.GetComponent<PlayerController>();
                    player.TakeDamage();
                    _enemySpawner.DestroyEnemy(this);
                    _hasDeductedHealth = true;
                }
            }
        }

        private void Move()
        {
            // find nearest player of the 2 players
            var distancePlayer1 = Vector3.Distance(transform.position, _players[0].transform.position);
            var distancePlayer2 = Vector3.Distance(transform.position, _players[1].transform.position);

            _target = distancePlayer1 < distancePlayer2 ? _players[0] : _players[1];

            if (_target == null) return;
            // look at the target
            transform.LookAt(_target.transform);
            // move towards the target
            _controller.Move((_speed * Time.deltaTime) * transform.forward);
        }
    }
}