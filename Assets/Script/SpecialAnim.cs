using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpecialAnim : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    Animator anim;

    int is_sit;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        is_sit = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }

        //if (Input.anyKey) anim.SetBool("dance1", false);
        //else anim.SetBool("dance1", true);
        if (Input.GetKeyDown(KeyCode.Space)) anim.SetTrigger("jump");
        else if (Input.GetKeyDown(KeyCode.F1)) anim.SetTrigger("dance");
        else if (Input.GetKeyDown(KeyCode.F2)) anim.SetTrigger("gangnam");
        else if (Input.GetKeyDown(KeyCode.F3)) anim.SetTrigger("standing_clap");
        else if (Input.GetKeyDown(KeyCode.F4)) anim.SetTrigger("crawl");
        else if (Input.GetKeyDown(KeyCode.F5)) anim.SetTrigger("hurricane");
        else if (Input.GetKeyDown(KeyCode.F6)) anim.SetTrigger("fireball");
        else if (Input.GetKeyDown(KeyCode.F7)) anim.SetTrigger("plank");
        else if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            if (is_sit == 0) anim.SetTrigger("stand_to_sit");
            else anim.SetTrigger("sit_to_stand");
            is_sit = 1 - is_sit;
        }
    }
}
