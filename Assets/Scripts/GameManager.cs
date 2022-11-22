using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        //Insert functions for loading scene for avatar creation
        //현재는 photoninit에서 loadscene하고 여기서 avatar 생성.
        //photoninit의 select tema 에서 create room 하기 전에 avatar 저작 기능 씬으로 이동했다가 가기
        //태곤이 구현해놓은 아바타 씬이동 코드 분석부터
        CreateMale();
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isChatPanelActive)
            {
                isChatPanelActive = true;
                Debug.Log(isChatPanelActive);
                ChatPanel.SetActive(true);
            }
            else
            {
                Send();
            }
        }
    }

    void CreateMale()
    {
        PhotonNetwork.Instantiate("Male", new Vector3(688.66f, 30f, 692.83f), Quaternion.identity);
    }

    #region 채팅
    //update에서 enter 키 입력 시 채팅 창 켜짐
    public GameObject ChatPanel;

    public TMP_Text[] ChatText;
    public TMP_InputField ChatInput;
    public PhotonView PV;

    private bool isChatPanelActive = false;

    public void Send()
    {
        Debug.Log(ChatInput.text.ToString());
        if (ChatInput.text.ToString() == "")
        {
            Debug.Log("????");
            isChatPanelActive = false;
            ChatPanel.SetActive(false); 
            Debug.Log(isChatPanelActive);

        }
        else
        {
            PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
            ChatInput.text = "";
        }
    }

    [PunRPC] // RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
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
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion
}
