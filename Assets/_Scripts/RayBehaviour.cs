using Photon.Pun;
using UnityEngine;

namespace Assets._Scripts
{
    [RequireComponent(typeof(LineRenderer)), RequireComponent(typeof(BoxCollider))]
    public class RayBehaviour : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private BoxCollider _boxCollider;

        private Vector3 _startPosition, _endPosition;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount < 2) return;
            _startPosition = _lineRenderer.GetPosition(0);
            _endPosition = _lineRenderer.GetPosition(1);

            // calculate the angle between x axis and the line
            var angle = Mathf.Atan2(_endPosition.z - _startPosition.z, _endPosition.x - _startPosition.x) * 180 / Mathf.PI;
            // set the angle to the transform
            transform.eulerAngles = new Vector3(0, -angle, 0);

            var midPoint = (_startPosition + _endPosition) / 2;

            transform.position = midPoint;

            _boxCollider.size = new Vector3(Vector3.Distance(_startPosition, _endPosition), _boxCollider.size.y, _boxCollider.size.z);
        }
    }
}