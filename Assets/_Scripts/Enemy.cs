using UnityEngine;

namespace Assets._Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class Enemy : MonoBehaviour
    {
        private PlayerController[] _players;
        private PlayerController _target;
        private CharacterController _controller;

        [SerializeField] private float _speed = 5f;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            _players = FindObjectsOfType<PlayerController>();
        }

        private void Update()
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