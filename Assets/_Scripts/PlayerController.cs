using System.Collections;
using Photon.Pun;
using UnityEngine;

namespace Assets._Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _speed = 5f;

        private PhotonView _photonView;
        private Health _health;
        private RayBehaviour _rayBehaviour;

        private void Start()
        {
            _health = FindObjectOfType<Health>();
            _photonView = GetComponent<PhotonView>();
            _controller = GetComponent<CharacterController>();
            StartCoroutine(WaitForTime());
        }

        private void Update()
        {
            if (_photonView.IsMine)
            {
                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");
                var direction = new Vector3(horizontal, 0, vertical).normalized;
                _controller.Move((_speed * Time.deltaTime) * direction);
                if (_rayBehaviour == null) return;
                var rendererPosition = new Vector3(transform.position.x, 1f, transform.position.z);
                _rayBehaviour.GetP1Position(rendererPosition);
            }
            else
            {
                if (_rayBehaviour == null) return;
                var rendererPosition = new Vector3(transform.position.x, 1f, transform.position.z);
                _rayBehaviour.GetP2Position(rendererPosition);
            }
        }

        public void TakeDamage()
        {
            if (!_photonView.IsMine) return;
            _health.TakeDamage();
        }

        private IEnumerator WaitForTime()
        {
            yield return new WaitForSeconds(0.1f);
            _rayBehaviour = GameObject.FindGameObjectWithTag("Ray").GetComponent<RayBehaviour>();
        }
    }
}