using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class SlotScript : MonoBehaviour
{
    public GameObject human;
    public GameObject male;
    public GameObject female;
    public GameObject MaleHair;
    public GameObject MaleHead;
    public GameObject MaleTop;
    public GameObject MaleBottom;
    public GameObject FemaleHair;
    public GameObject FemaleHead;
    public GameObject FemaleTop;
    public GameObject FemaleBottom;

    List<GameObject> mheads = new List<GameObject>();
    List<GameObject> mtops = new List<GameObject>();
    List<GameObject> mlegs = new List<GameObject>();
    List<GameObject> mhairs = new List<GameObject>();
    List<GameObject> fheads = new List<GameObject>();
    List<GameObject> ftops = new List<GameObject>();
    List<GameObject> flegs = new List<GameObject>();
    List<GameObject> fhairs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        human = gameObject;
        male = human.transform.Find("Male").gameObject;
        female = human.transform.Find("Female").gameObject;
        Transform[] allmalechild = male.GetComponentsInChildren<Transform>();
        Transform[] allfemalechild = female.GetComponentsInChildren<Transform>();
        int cnt = 0;
        foreach (Transform child in allmalechild)
        {
            if (child.name == "Hair")
            {
                MaleHair = child.gameObject;
            }
            else if (child.name == "Head")
            {
                if (cnt == 0)
                {
                    MaleHead = child.gameObject;
                }
                cnt++;
            }
            else if (child.name == "Top")
            {
                MaleTop = child.gameObject;
            }
            else if (child.name == "Legs")
            {
                MaleBottom = child.gameObject;
            }

        }
        cnt = 0;
        foreach (Transform child in allfemalechild)
        {
            if (child.name == "Hair")
            {
                FemaleHair = child.gameObject;
            }
            else if (child.name == "Head")
            {
                if (cnt == 0)
                {
                    FemaleHead = child.gameObject;
                }
                cnt++;
            }
            else if (child.name == "Top")
            {
                FemaleTop = child.gameObject;
            }
            else if (child.name == "Legs")
            {
                FemaleBottom = child.gameObject;
            }

        }

        makeList(MaleHair.transform, mhairs);
        makeList(MaleHead.transform, mheads);
        makeList(MaleTop.transform, mtops);
        makeList(MaleBottom.transform, mlegs);
        makeList(FemaleHair.transform, fhairs);
        makeList(FemaleHead.transform, fheads);
        makeList(FemaleTop.transform, ftops);
        makeList(FemaleBottom.transform, flegs);


    }
    void makeList(Transform group, List<GameObject> dressList)
    {
        foreach (Transform dress in group)
        {
            dressList.Add(dress.gameObject);
        }
    }
    public void ChangeHairSelect()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.name);
        string strText = clickObject.name;
        string strNum = "";
        strNum = Regex.Replace(strText, @"\D", "");
        print(strNum);
        int num = int.Parse(strNum) - 1;
        if (transform.GetChild(0).gameObject.activeSelf == true)
        {
            print("AAAAA");
            transform.GetChild(0).GetComponent<character>().currentHairNumber = num;
            print("KKKKKKKK");
            print(mhairs.Count);
            transform.GetChild(0).gameObject.GetComponent<character>().ShowDresses(mhairs, num);
            print("BBBBB");
        }
        else
        {
            print("CCCCCC");
            female.GetComponent<character>().currentHairNumber = num;
            female.GetComponent<character>().ShowDresses(fhairs, num);
            print("DDDDD");
        }
    }
    public void ChangeHeadSelect()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.name);
        string strText = clickObject.name;
        string strNum = "";
        strNum = Regex.Replace(strText, @"\D", "");
        print(strNum);
        int num = int.Parse(strNum) - 1;
        if (human.transform.Find("Male").gameObject.activeSelf == true)
        {
            male.GetComponent<character>().currentHeadNumber = num;
            male.GetComponent<character>().ShowDresses(mheads, num);
        }
        else
        {
            female.GetComponent<character>().currentHeadNumber = num;
            female.GetComponent<character>().ShowDresses(fheads, num);
        }
    }
    public void ChangeTopSelect()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.name);
        string strText = clickObject.name;
        string strNum = "";
        strNum = Regex.Replace(strText, @"\D", "");
        print(strNum);
        int num = int.Parse(strNum) - 1;
        if (human.transform.Find("Male").gameObject.activeSelf == true)
        {
            male.GetComponent<character>().currentTopNumber = num;
            male.GetComponent<character>().ShowDresses(mtops, num);
        }
        else
        {
            female.GetComponent<character>().currentTopNumber = num;
            female.GetComponent<character>().ShowDresses(ftops, num);
        }
    }
    public void ChangeBottomSelect()
    {
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.name);
        string strText = clickObject.name;
        string strNum = "";
        strNum = Regex.Replace(strText, @"\D", "");
        print(strNum);
        int num = int.Parse(strNum) - 1;
        if (human.transform.Find("Male").gameObject.activeSelf == true)
        {
            male.GetComponent<character>().currentLegNumber = num;
            male.GetComponent<character>().ShowDresses(mlegs, num);
        }
        else
        {
            female.GetComponent<character>().currentLegNumber = num;
            female.GetComponent<character>().ShowDresses(flegs, num);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}