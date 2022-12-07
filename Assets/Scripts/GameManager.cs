using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    private int isCreator;

    public GameObject VoiceObject;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManger Start");
        //Insert functions for loading scene for avatar creation
        //����� photoninit���� loadscene�ϰ� ���⼭ avatar ����.
        //photoninit�� select tema ���� create room �ϱ� ���� avatar ���� ��� ������ �̵��ߴٰ� ����
        //�°��� �����س��� �ƹ�Ÿ ���̵� �ڵ� �м�����
        // MapEdit();

        isCreator = PlayerPrefs.GetInt("isCreator");
        Debug.Log("isCreator");
        Debug.Log(isCreator);
        if (PlayerPrefs.GetInt("isVoice") == 0)
        {
            VoiceObject.SetActive(false);
        }

        Debug.Log("isCreator");
        Debug.Log(isCreator);
        if (isCreator == 0)
        {
            GameObject.Find("WorldEdit").SetActive(false);
            CreateHuman();
            
            // 1. 물건 생성하는 panel off
            // 2. 카메라 이동작업
        }
        else if (isCreator == 1)
        {
            GameObject.Find("Camera").GetComponent<CameraControl>().editCameraOn();
        }
        //PhotonNetwork.IsMessageQueueRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isCreator == 1 && Input.GetKeyDown(KeyCode.M))
        {
            CreateHuman();
        }
        */
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isChatPanelActive)
            {
                isChatPanelActive = true;
                ChatPanel.SetActive(true);
            }
            else
            {
                Send();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPanel.SetActive(true);
        }
    }

    #region QUIT_GAME
    public GameObject QuitPanel;

    public void QuitPanelOff()
    {
        QuitPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    void MapEdit()
    {

    }

    public void CreateHuman()
    {
        PhotonNetwork.Instantiate("human", new Vector3(688.66f, 10f, 692.83f), Quaternion.identity);

        GameObject.Find("CameraController").GetComponent<CameraControl>().mainCameraOn();
    }

    #region CHAT
    //update���� enter Ű �Է� �� ä�� â ����
    public GameObject ChatPanel;

    public TMP_Text[] ChatText;
    public TMP_InputField ChatInput;
    public PhotonView PV;

    private bool isChatPanelActive = false;

    public void Send()
    {
        if (ChatInput.text.ToString() == "")
        {
            isChatPanelActive = false;
            ChatPanel.SetActive(false); 
        }
        else
        {
            PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
            ChatInput.text = "";
        }
    }

    [PunRPC] // RPC�� �÷��̾ �����ִ� �� ��� �ο����� �����Ѵ�
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if (!isInput) // ������ ��ĭ�� ���� �ø�
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion
}
