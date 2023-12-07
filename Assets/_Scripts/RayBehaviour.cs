using Photon.Pun;
using UnityEngine;

namespace Assets._Scripts
{
    [RequireComponent(typeof(LineRenderer)), RequireComponent(typeof(BoxCollider))]
    public class RayBehaviour : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private BoxCollider _boxCollider;

        private Vector3 _p1Position;
        private Vector3 _p2Position;

        private Vector3 _startPosition, _endPosition;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
            {
                _lineRenderer.SetPosition(0, -1 * Vector3.up);
                _lineRenderer.SetPosition(1, -1 * Vector3.up);
                return;
            }

            _lineRenderer.SetPosition(0, _p1Position);
            _lineRenderer.SetPosition(1, _p2Position);
            _startPosition = _p1Position;
            _endPosition = _p2Position;

            // calculate the angle between x axis and the line
            var angle = Mathf.Atan2(_endPosition.z - _startPosition.z, _endPosition.x - _startPosition.x) * 180 / Mathf.PI;
            // set the angle to the transform
            transform.eulerAngles = new Vector3(0, -angle, 0);

            var midPoint = (_startPosition + _endPosition) / 2;

            transform.position = midPoint;

            _boxCollider.size = new Vector3(Vector3.Distance(_startPosition, _endPosition), _boxCollider.size.y, _boxCollider.size.z);
        }

        public void GetP1Position(Vector3 position)
        {
            _p1Position = position;
        }

        public void GetP2Position(Vector3 position)
        {
            _p2Position = position;
        }
    }
}