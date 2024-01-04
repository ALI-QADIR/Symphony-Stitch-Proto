using System.Collections;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Assets._Scripts
{
    public class Health : MonoBehaviourPunCallbacks
    {
        public static bool isGameActive;

        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _countdownText;
        [SerializeField] private TMP_Text _waitingText;
        [SerializeField] private TMP_Text _gameOverText;
        [SerializeField] private int _maxHealth = 10;
        private int _currentHealth;
        private int _serverCountdown = 3;
        private int _localCountdown = 3;
        private int _localTime = 0;
        private int _serverTime = 0;
        private bool _countdownBegun;

        private Coroutine _countdownCoroutine;
        private Coroutine _timerCoroutine;

        private PhotonView _photonView;

        private void Start()
        {
            _photonView = GetComponent<PhotonView>();
            _currentHealth = _maxHealth;
            _healthText.text = _currentHealth.ToString();
            _timeText.text = _localTime.ToString();
            _countdownText.text = _localCountdown.ToString();
            _timeText.gameObject.SetActive(false);
            _countdownText.gameObject.SetActive(false);
            _healthText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_currentHealth <= 0 && isGameActive)
            {
                isGameActive = false;
                GameOver("You Died.", _localTime);
                StopCoroutine(_timerCoroutine);
            }

            if (PhotonNetwork.CurrentRoom.PlayerCount < 2 && _countdownBegun && isGameActive)
            {
                isGameActive = false;
                GameOver("Player 2 Left.", _localTime);
                StopCoroutine(_timerCoroutine);
            }
            else if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && !_countdownBegun)
            {
                isGameActive = true;
                _countdownBegun = true;
                _countdownText.gameObject.SetActive(true);
                _waitingText.gameObject.SetActive(false);
                _countdownText.text = _localCountdown.ToString();
                _countdownCoroutine = StartCoroutine(Countdown());
            }
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

        [PunRPC]
        private void UpdateCountdownRpc()
        {
            _serverCountdown = _localCountdown;
        }

        [PunRPC]
        private void UpdateTimeRpc()
        {
            _serverTime = _localTime;
        }

        private void GameOver(string text, int score)
        {
            _gameOverText.text = text + " Your Score: " + score.ToString();
            _gameOverText.gameObject.SetActive(true);
        }

        private IEnumerator Countdown()
        {
            while (_serverCountdown > 0)
            {
                yield return new WaitForSeconds(1f);
                _localCountdown--;
                _countdownText.text = _localCountdown.ToString();
                _photonView.RPC("UpdateCountdownRpc", RpcTarget.All);
            }

            _countdownText.gameObject.SetActive(false);
            _timeText.gameObject.SetActive(true);
            _healthText.gameObject.SetActive(true);
            _timerCoroutine = StartCoroutine(CountTime());
            StopCoroutine(_countdownCoroutine);
        }

        private IEnumerator CountTime()
        {
            while (_currentHealth > 0)
            {
                yield return new WaitForSeconds(1f);
                _localTime++;
                _timeText.text = "Score: " + _localTime.ToString();
                _photonView.RPC("UpdateTimeRpc", RpcTarget.All);
            }
        }
    }
}