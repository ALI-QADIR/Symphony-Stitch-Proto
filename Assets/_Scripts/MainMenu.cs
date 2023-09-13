using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Assets._Scripts
{
    public class MainMenu : MonoBehaviourPunCallbacks
    {
        [SerializeField] private TMP_InputField _createRoomInputField;
        [SerializeField] private TMP_InputField _joinRoomInputField;

        public void CreateRoom()
        {
            // TODO: use Try Catch and display a panel popup if error
            if (string.IsNullOrEmpty(_createRoomInputField.text))
            {
                return;
            }

            var roomOptions = new RoomOptions
            {
                MaxPlayers = 2
            };

            PhotonNetwork.CreateRoom(_createRoomInputField.text, roomOptions);
        }

        public void JoinRoom()
        {
            //TODO: use Try Catch and display a panel popup if error
            if (string.IsNullOrEmpty(_joinRoomInputField.text))
            {
                return;
            }
            
            PhotonNetwork.JoinRoom(_joinRoomInputField.text);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}