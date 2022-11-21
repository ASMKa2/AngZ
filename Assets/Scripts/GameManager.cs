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
        CreateMale();
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateMale()
    {
        PhotonNetwork.Instantiate("Male", new Vector3(688.66f, 30f, 692.83f), Quaternion.identity);
    }
}
