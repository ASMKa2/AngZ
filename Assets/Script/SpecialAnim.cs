using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAnim : MonoBehaviour
{

    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {

        if(Input.anyKey) anim.SetBool("dance1",false);
        else anim.SetBool("dance1",true);
        if (Input.GetKeyDown(KeyCode.F)) anim.SetTrigger("dance");
        else if (Input.GetKeyDown(KeyCode.Space)) anim.SetTrigger("jump");
        
    }
}
