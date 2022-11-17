using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 120f;
    bool isWalking = false;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    bool IsWalking(float hAxisValue, float vAxisValue){
        if(Mathf.Abs(hAxisValue)>0.5f || Mathf.Abs(vAxisValue)>0.5f){
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        float axisValueHorizontal = Input.GetAxis("Horizontal");
        float axisValueVertical = Input.GetAxis("Vertical");
        
        float speedPerSeconds = speed * Time.deltaTime;
        float rotSpeedPerSecond = rotateSpeed * Time.deltaTime;
        float xSpeed = rotSpeedPerSecond * axisValueHorizontal;
        float ySpeed = 0;


        isWalking = IsWalking(axisValueHorizontal,axisValueVertical);
        anim.SetBool("run",isWalking);
        if(Input.GetKey(KeyCode.LeftShift)){
            anim.SetBool("walk",true);
            anim.SetBool("run",false);
            ySpeed = speedPerSeconds * axisValueVertical*0.4f;
        }else{
            anim.SetBool("walk",false);
            ySpeed = speedPerSeconds * axisValueVertical;
        }

        transform.Translate(0,0,ySpeed);
        transform.Rotate(0,xSpeed,0);
    }
}
