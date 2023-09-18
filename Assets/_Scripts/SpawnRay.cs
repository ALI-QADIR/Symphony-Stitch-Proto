using Photon.Pun;
using UnityEngine;

namespace Assets._Scripts
{
    public class SpawnRay : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            PhotonNetwork.Instantiate("Ray", new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }
}
