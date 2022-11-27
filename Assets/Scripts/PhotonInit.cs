using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PhotonInit : MonoBehaviourPunCallbacks
{

    public enum ActivePanel
    {
        LOGIN = 0,
        INIT = 1,
        CREATE_ROOM = 2,
        ROOM_LIST = 3,
        ROOM_OPTIONS = 4,
        PASSWORD = 5,
        PASSWORD_WRONG = 6
    }
    public ActivePanel activePanel = ActivePanel.LOGIN;

    private string gameVersion = "1.0";
    public string userId = "AngZ";
    public byte maxPlayer = 20;

    public TMP_InputField txtUserId;
    public TMP_InputField txtRoomName;
    public TMP_InputField txtMaxPlayers;

    public Toggle toggleLocked;
    public TMP_InputField password;

    public TMP_InputField passwordTried;

    public GameObject[] panels; //login~���� ���� ���������� panels

    private void Awake()
    {
        // photon1�� photon2�� �ٲ�鼭 �޶����� (������ ����ȭ)
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        // �������̵� �ۼ����� ������ �������� ID ����
        PhotonNetwork.ConnectUsingSettings();

        txtUserId.text = PlayerPrefs.GetString("USER_ID", "USER_" + Random.Range(1, 999));
        txtRoomName.text = PlayerPrefs.GetString("ROOM_NAME", "ROOM_" + Random.Range(1, 999));
        password.text = PlayerPrefs.GetString("PASSWORD", "");


    }

    #region SELF_CALLBACK_FUNCTIONS
    public void OnLogin()
    {
        Debug.Log("login");
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.NickName = txtUserId.text;


        PlayerPrefs.SetString("USER_ID", PhotonNetwork.NickName);
        ChangePanel(ActivePanel.INIT);

    }

    public void OnBackToInit()
    {
        ChangePanel(ActivePanel.INIT);
    }

    public void OnBackToRoomOptions()
    {
        ChangePanel(ActivePanel.ROOM_OPTIONS);
    }

    public void OnRoomOptionsClick()
    {
        ChangePanel(ActivePanel.ROOM_OPTIONS);
    }

    public void ToggleClick(bool isOn)
    {
        int isLocked;
        if (isOn)
        {
            Debug.Log("1");

            isLocked = 1;
        }
        else
        {
            Debug.Log("0");

            isLocked = 0;
        }
        PlayerPrefs.SetInt("IS_LOCKED", isLocked);
        Debug.Log(PlayerPrefs.GetInt("IS_LOCKED"));
        Debug.Log(isLocked);
    }

    public void OnCreateRoomClick()
    {
        Debug.Log(txtMaxPlayers.text);
        PlayerPrefs.SetString("ROOM_NAME", txtRoomName.text);

        ChangePanel(ActivePanel.CREATE_ROOM);
    }

    public void OnRoomListClick()
    {
        if (PhotonNetwork.IsConnected)
        {
            ChangePanel(ActivePanel.ROOM_LIST);
            MyListRenewal();
        }
        else
        {
            Debug.Log("Not connected to network yet!!");
        }
    }

    public void OnCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        byte[] players = System.Text.Encoding.UTF8.GetBytes(txtMaxPlayers.text);

        byte result = 0;

        if (players.Length > 2)
        {
            result = 99;
        }
        else
        {
            for (int i = 0; i < players.Length; i++)
            {
                result *= 10;
                result += (byte)(players[i] - (byte)48);
            }
        }

        roomOptions.MaxPlayers = result;

        roomOptions.CustomRoomProperties = new Hashtable() {
            {"roomName", txtRoomName.text },
            {"isLocked", toggleLocked.isOn },
            {"password", password.text }
        };
        roomOptions.CustomRoomPropertiesForLobby = new string[] {
            "roomName",
            "isLocked",
            "password",
        };
        PhotonNetwork.CreateRoom(txtRoomName.text
                                , roomOptions);
        Debug.Log(roomOptions.MaxPlayers);
    }
    public void SelectTema_1()
    {
        OnCreateRoom();
        sceneName = "Scene_LectureRoom";
    }
    public void SelectTema_2()
    {
        OnCreateRoom();
        sceneName = "Scene_Nature";

    }
    public void SelectTema_3()
    {
        OnCreateRoom();
        sceneName = "Scene_Winter";

    }
    public void SelectTema_4()
    {
        OnCreateRoom();
        sceneName = "Scene_Street";

    }

    #endregion

    private void ChangePanel(ActivePanel panel)
    {
        foreach (GameObject _panel in panels)
        {
            _panel.SetActive(false);
        }
        panels[(int)panel].SetActive(true);
    }

    #region PHOTON_CALLBACK_FUNCTIONS
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect To Master");
        // PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        myList.Clear();
    }

    public string sceneName;

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room !!!");
        // photonNetwork�� ������ ����� ��� ���� �����ش�. 
        // gamemanager���� avatar�� instantiate�ϰ� ���� �ٽ� �����Ų��
        PhotonNetwork.IsMessageQueueRunning = false; 
        SceneManager.LoadScene(sceneName);
    }

    #endregion

    private void PasswordPanelOn()
    {
        panels[(int)ActivePanel.PASSWORD].SetActive(true);
    }

    public void PasswordPanelOff()
    {
        panels[(int)ActivePanel.PASSWORD].SetActive(false);
        panels[(int)ActivePanel.PASSWORD_WRONG].SetActive(false);
    }

    public void OnPasswordClick()
    {
        Hashtable ht = myList[curRoomNum].CustomProperties;
        Debug.Log(ht["password"]);
        Debug.Log(passwordTried.text);
        if (passwordTried.text.Equals(System.Convert.ToString(ht["password"])))
        {
            panels[(int)ActivePanel.PASSWORD_WRONG].SetActive(false);
            PhotonNetwork.JoinRoom(myList[curRoomNum].Name);
        }
        else
        {
            //message for password wrong
            panels[(int)ActivePanel.PASSWORD_WRONG].SetActive(true);
        }

        

    }

    public GameObject LobbyPanel;
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;
    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;
    int curRoomNum;
    #region �渮��Ʈ ����
    // ����ư -2 , ����ư -1 , �� ����
    public void MyListClick(int num)
    {
        if (num == -2)
        {
            --currentPage; 
            MyListRenewal();
        }
        else if (num == -1)
        {
            ++currentPage;
            MyListRenewal();
        }
        else
        {
            Hashtable ht = myList[multiple + num].CustomProperties;
            Debug.Log(ht["isLocked"].GetType());
            curRoomNum = multiple + num;

            bool isLocked = System.Convert.ToBoolean(ht["isLocked"]);


            if (isLocked)
            {
                Debug.Log(ht["password"]);
                PasswordPanelOn();
            }
            else
            {
                PhotonNetwork.JoinRoom(myList[multiple + num].Name);
                MyListRenewal();
            }
            //PhotonNetwork.JoinRoom(myList[multiple + num].Name);

        }
    }

    void MyListRenewal()
    {
        // �ִ�������
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // ����, ������ư
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // �������� �´� ����Ʈ ����
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<TMP_Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<TMP_Text>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }
    #endregion
 
    
}