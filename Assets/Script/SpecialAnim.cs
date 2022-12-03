using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpecialAnim : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        if (Input.anyKey) anim.SetBool("dance1", false);
        else anim.SetBool("dance1", true);
        if (Input.GetKeyDown(KeyCode.F1)) anim.SetTrigger("dance");
        else if (Input.GetKeyDown(KeyCode.Space)) anim.SetTrigger("jump");
    }
}
