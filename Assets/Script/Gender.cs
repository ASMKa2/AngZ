using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gender : MonoBehaviour
{
    private int gender; // 1 : ���� 2 : ���� 0 : playerprefs�� �޾ƿ� �⺻��

    public GameObject Male;
    public GameObject Female;
    // Start is called before the first frame update
    void Start()
    {
        InitGender();
        Male = GameObject.Find("Male");
        Female = GameObject.Find("Female");
        if (gender == 1)
        {
            Female.SetActive(false);
        }
        else if (gender == 2)
        {
            Male.SetActive(false);
        }
    }

    public void InitGender()
    {
        gender = PlayerPrefs.GetInt("gender");
        if (gender == 0) gender = 1;
    }

    // ���� ���ڸ� ���ڷ�, ���� ���ڸ� ���ڷ� ����
    public void GenderChange()
    {
        if (gender == 1)
        {
            Female.SetActive(true);
            Male.SetActive(false);
            gender = 2;
        }
        else if (gender == 2)
        {
            Male.SetActive(true);
            Female.SetActive(false);
            gender = 1;
        }
        PlayerPrefs.SetInt("gender", gender);
    }

    /*
     * Change~ �κ�
     * gender�� ���� �ش� gender �ڽ��� character �ȿ� �ִ� change~() �Լ��� ȣ�����ش�.

    */

    public void ChangeHair()
    {
        if (gender == 1) GameObject.Find("Male").GetComponent<character>().ChangeHair();
        else if (gender == 2) GameObject.Find("Female").GetComponent<character>().ChangeHair();
        else return;
    }

    public void ChangeHead()
    {
        if (gender == 1) GameObject.Find("Male").GetComponent<character>().ChangeHead();
        else if (gender == 2) GameObject.Find("Female").GetComponent<character>().ChangeHead();
        else return;
    }

    public void ChangeTop()
    {
        if (gender == 1) GameObject.Find("Male").GetComponent<character>().ChangeTop();
        else if (gender == 2) GameObject.Find("Female").GetComponent<character>().ChangeTop();
        else return;
    }

    public void ChangeLeg()
    {
        if (gender == 1) GameObject.Find("Male").GetComponent<character>().ChangeLeg();
        else if (gender == 2) GameObject.Find("Female").GetComponent<character>().ChangeLeg();
        else return;
    }

    public void Save()
    {
        if (gender == 1) GameObject.Find("Male").GetComponent<character>().SaveCurrentDresses();
        else if (gender == 2) GameObject.Find("Female").GetComponent<character>().SaveCurrentDresses();
        else return;
    }

    public void ChangeRoom()
    {
        if (gender == 1) GameObject.Find("Male").GetComponent<character>().BackAdornment();
        else if (gender == 2) GameObject.Find("Female").GetComponent<character>().BackAdornment();
        else return;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
