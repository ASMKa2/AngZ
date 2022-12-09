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
        else if (Input.GetKeyDown(KeyCode.F8)) anim.SetTrigger("kick");
        else if (Input.GetKeyDown(KeyCode.F9)) anim.SetTrigger("kick");
        else if (Input.GetKeyDown(KeyCode.F10)) anim.SetTrigger("kick");
        else if (Input.GetKeyDown(KeyCode.F11)) anim.SetTrigger("kick");
        else if (Input.GetKeyDown(KeyCode.F12)) anim.SetTrigger("kick");

    }
}
