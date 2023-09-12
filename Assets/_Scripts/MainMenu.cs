using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

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
            Debug.Log("Join Room Input Field is empty");
            return;
        }

        Debug.Log("Joining Room: " + _joinRoomInputField.text);
        PhotonNetwork.JoinRoom(_joinRoomInputField.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}