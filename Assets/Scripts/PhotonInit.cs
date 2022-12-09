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
        PASSWORD_WRONG = 6,
        LOADING = 7,
        QUIT = 8,
        PASSWORD_EMPTY = 9
    }
    public ActivePanel activePanel = ActivePanel.LOGIN;

    private string gameVersion = "1.0";
    public string userId = "AngZ";
    public byte maxPlayer = 20;

    public TMP_InputField txtUserId;
    public TMP_InputField txtRoomName;
    public TMP_InputField txtMaxPlayers;

    public Toggle toggleLocked;
    public Toggle toggleVoice;
    public TMP_InputField password;

    public TMP_InputField passwordTried;

    public GameObject[] panels; //login~월드 입장 직전까지의 panels

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        // photon1과 photon2로 바뀌면서 달라진점 (같은방 동기화)
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        // 유저아이디를 작성하지 않으면 랜덤으로 ID 적용
        PhotonNetwork.ConnectUsingSettings();

        txtUserId.text = PlayerPrefs.GetString("USER_ID", "USER_" + Random.Range(1, 999));
        txtRoomName.text = PlayerPrefs.GetString("ROOM_NAME", "ROOM_" + Random.Range(1, 999));
        password.text = PlayerPrefs.GetString("PASSWORD", "");

        PlayerPrefs.SetInt("isCreator", 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panels[(int)ActivePanel.QUIT].SetActive(true);
        }
    }

    public void QuitPanelOff()
    {
        panels[(int)ActivePanel.QUIT].SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
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
            isLocked = 1;
        }
        else
        {
            isLocked = 0;
        }
        PlayerPrefs.SetInt("IS_LOCKED", isLocked);
    }

    public void OnCreateRoomClick()
    {
        if(password.text == "" && toggleLocked.isOn)
        {
            panels[(int)ActivePanel.PASSWORD_EMPTY].SetActive(true);
            return;
        }
        else
        {
            panels[(int)ActivePanel.PASSWORD_EMPTY].SetActive(false);
        }

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
        PlayerPrefs.SetInt("isCreator", 1);

        if (toggleVoice.isOn)
        {
            PlayerPrefs.SetInt("isVoice", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isVoice", 0);
        }

        RoomOptions roomOptions = new RoomOptions();
        byte[] players = System.Text.Encoding.UTF8.GetBytes(txtMaxPlayers.text);

        byte result = 0;

        if (players.Length > 2 || players.Length <= 0)
        {
            result = 10;
        }
        else
        {
            for (int i = 0; i < players.Length; i++)
            {
                result *= 10;
                result += (byte)(players[i] - (byte)48);
            }
        }

        if (result > 10)
        {
            result = 10;
        }

        roomOptions.MaxPlayers = result;

        roomOptions.CustomRoomProperties = new Hashtable() {
            {"roomName", txtRoomName.text },
            {"isLocked", toggleLocked.isOn },
            {"isVoice", toggleVoice.isOn },
            {"password", password.text },
            {"sceneName",sceneName}
        };
        roomOptions.CustomRoomPropertiesForLobby = new string[] {
            "roomName",
            "isLocked",
            "isVoice",
            "password",
            "sceneName"
        };
        PhotonNetwork.CreateRoom(txtRoomName.text
                                , roomOptions);
    }
    public void SelectTema_1()
    {
        sceneName = "LectureRoom";
        OnCreateRoom();
    }
    public void SelectTema_2()
    {
        sceneName = "Nature";
        OnCreateRoom();

    }
    public void SelectTema_3()
    {
        sceneName = "Winter";
        OnCreateRoom();

    }
    public void SelectTema_4()
    {
        sceneName = "ArtGallery";
        OnCreateRoom();

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
        ChangePanel(ActivePanel.LOGIN);

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
        // photonNetwork의 데이터 통신을 잠깐 정지 시켜준다. 
        // gamemanager에서 avatar를 instantiate하고 나면 다시 연결시킨다
        //PhotonNetwork.IsMessageQueueRunning = false; 
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
    public List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;
    int curRoomNum;
    #region 방리스트 갱신
    // ◀버튼 -2 , ▶버튼 -1 , 셀 숫자
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
            curRoomNum = multiple + num;

            bool isLocked = System.Convert.ToBoolean(ht["isLocked"]);

            if (isLocked)
            {
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
        // 최대페이지
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // 이전, 다음버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            if (multiple + i < myList.Count)
            {
                Hashtable ht = myList[multiple + i].CustomProperties;
                bool isLocked = System.Convert.ToBoolean(ht["isLocked"]);
                bool isVoice = System.Convert.ToBoolean(ht["isVoice"]);

                if (isLocked)
                {
                    CellBtn[i].transform.GetChild(2).GetComponent<RawImage>().enabled = true;
                }
                else
                {
                    CellBtn[i].transform.GetChild(2).GetComponent<RawImage>().enabled = false;
                }
                if (isVoice)
                {
                    CellBtn[i].transform.GetChild(3).GetComponent<RawImage>().enabled = true;
                }
                else
                {
                    CellBtn[i].transform.GetChild(3).GetComponent<RawImage>().enabled = false;
                }
            }
            else
            {
                CellBtn[i].transform.GetChild(2).GetComponent<RawImage>().enabled = false;
                CellBtn[i].transform.GetChild(3).GetComponent<RawImage>().enabled = false;
            }
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