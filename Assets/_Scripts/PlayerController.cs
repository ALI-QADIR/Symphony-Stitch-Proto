using Photon.Pun;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets._Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float _speed = 5f;

        private LineRenderer _lineRenderer;
        private PhotonView _photonView;
        private Health _health;

        private void Start()
        {
            _health = FindObjectOfType<Health>();
            _photonView = GetComponent<PhotonView>();
            _lineRenderer = FindObjectOfType<LineRenderer>();
        }

        private void Update()
        {
            if (_photonView.IsMine)
            {
                var horizontal = Input.GetAxis("Horizontal");
                var vertical = Input.GetAxis("Vertical");
                var direction = new Vector3(horizontal, 0, vertical).normalized;
                _controller.Move((_speed * Time.deltaTime) * direction);
                var rendererPosition = new Vector3(transform.position.x, 1f, transform.position.z);
                _lineRenderer.SetPosition(0, rendererPosition);
            }
            else
            {
                var rendererPosition = new Vector3(transform.position.x, 1f, transform.position.z);
                _lineRenderer.SetPosition(1, rendererPosition);
            }
        }

        private void TakeDamage()
        {
            _health.TakeDamage();
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (hit.gameObject.CompareTag("Enemy") && _photonView.IsMine)
            {
                TakeDamage();
                PhotonNetwork.Destroy(hit.gameObject);
            }
        }
    }
}