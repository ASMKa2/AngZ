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
        //CreateFemale();
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateFemale()
    {
        PhotonNetwork.Instantiate("Female 1 Variant", new Vector3(0, 3.0f, 0), Quaternion.identity);
    }
}
