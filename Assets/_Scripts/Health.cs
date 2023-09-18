using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Assets._Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private int _maxHealth = 10;
        private int _currentHealth;

        private PhotonView _photonView;

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _currentHealth = _maxHealth;
            _healthText.text = _currentHealth.ToString();
        }

        public void TakeDamage()
        {
            _photonView.RPC("TakeDamageRpc", RpcTarget.All);
        }

        [PunRPC]
        private void TakeDamageRpc()
        {
            _currentHealth--;
            _healthText.text = _currentHealth.ToString();
        }
    }
}