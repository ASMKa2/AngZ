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
        //����� photoninit���� loadscene�ϰ� ���⼭ avatar ����.
        //photoninit�� select tema ���� create room �ϱ� ���� avatar ���� ��� ������ �̵��ߴٰ� ����
        //�°��� �����س��� �ƹ�Ÿ ���̵� �ڵ� �м�����
        // MapEdit();
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

    void MapEdit()
    {

    }

    void CreateMale()
    {
        PhotonNetwork.Instantiate("human", new Vector3(688.66f, 30f, 692.83f), Quaternion.identity);
    }

    #region ä��
    //update���� enter Ű �Է� �� ä�� â ����
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
