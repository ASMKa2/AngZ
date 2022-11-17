using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class character : MonoBehaviour
{
    public Transform HeadGroup;
    public Transform TopGroup;
    public Transform LegGroup;
    public Transform HairGroup;

    List<GameObject> heads = new List<GameObject>();
    List<GameObject> tops = new List<GameObject>();
    List<GameObject> legs = new List<GameObject>();
    List<GameObject> hairs = new List<GameObject>();

    int currentHeadNumber = 0;
    int currentTopNumber = 0;
    int currentLegNumber = 0;
    int currentHairNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        MakeDressesAll();
        LoadSavedDresses();
        UpdateDresses();
        
    }


//#####################초기 의상 초기화 작업 #################################
    //의상 초기화 하기
    void MakeDresses(Transform group, List<GameObject> dressList){
        foreach(Transform dress in group){
            dress.gameObject.SetActive(false);  
            dressList.Add(dress.gameObject);
        }
    }
    // 한방에 초기화
    void MakeDressesAll(){
        MakeDresses(HeadGroup,heads);
        MakeDresses(TopGroup,tops);
        MakeDresses(LegGroup,legs);
        MakeDresses(HairGroup,hairs);
    }
    //선택한 number의 의상 나타내기
    void ShowDresses(List<GameObject> group, int dressNumber){
        for (int i=0;i<group.Count;i++){
            group[i].SetActive(false);
        }
        group[dressNumber].SetActive(true);
    }
    // 선택한 의상 한번에 나타내기
    void UpdateDresses(){
        ShowDresses(heads,currentHeadNumber);
        ShowDresses(tops,currentTopNumber);
        ShowDresses(legs,currentLegNumber);
        ShowDresses(hairs,currentHairNumber);
    }
//#########################################################################
//########################의상 변경하기#####################################
    //head nuber + 1한 head로 바꿔주기 Head 변경 버튼
    public void ChangeHead(){
        currentHeadNumber ++;
        if(currentHeadNumber > heads.Count - 1){
            currentHeadNumber = 0;
        }
        ShowDresses(heads,currentHeadNumber);
    }

    //Top 변경 버튼
    public void ChangeTop(){
        currentTopNumber ++;
        if(currentTopNumber > tops.Count - 1){
            currentTopNumber = 0;
        }
        ShowDresses(tops,currentTopNumber);
    }
    //Leg 변경 버튼
    public void ChangeLeg(){
        currentLegNumber ++;
        if(currentLegNumber > legs.Count - 1){
            currentLegNumber = 0;
        }
        ShowDresses(legs,currentLegNumber);
    }
    //Hair 변경 버튼
    public void ChangeHair(){
        currentHairNumber ++;
        if(currentHairNumber > hairs.Count - 1){
            currentHairNumber = 0;
        }
        ShowDresses(hairs,currentHairNumber);
    }

    public void ChangeSelectHair(){
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.name);
        string strText = clickObject.name;
        string strNum = "";
        strNum = Regex.Replace(strText,@"\D","");
        print(strNum);
        int num = int.Parse(strNum);
        currentHairNumber = num-1;
        ShowDresses(hairs,currentHairNumber);
    }
    public void ChangeSelectHead(){
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.name);
        string strText = clickObject.name;
        string strNum = "";
        strNum = Regex.Replace(strText,@"\D","");
        print(strNum);
        int num = int.Parse(strNum);
        currentHeadNumber = num-1;
        ShowDresses(heads,currentHeadNumber);
    }
    public void ChangeSelectTop(){
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.name);
        string strText = clickObject.name;
        string strNum = "";
        strNum = Regex.Replace(strText,@"\D","");
        print(strNum);
        int num = int.Parse(strNum);
        currentTopNumber = num-1;
        ShowDresses(tops,currentTopNumber);
    }
    public void ChangeSelectBottom(){
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        print(clickObject.name);
        string strText = clickObject.name;
        string strNum = "";
        strNum = Regex.Replace(strText,@"\D","");
        print(strNum);
        int num = int.Parse(strNum);
        currentLegNumber = num-1;
        ShowDresses(legs,currentLegNumber);
    }

//#################저장 및 씬 바꾸기################################
    //데이터 저장 및 불러오기 PlayerPrefs 사용한다
    //데이터 저장하기 (버튼과 연결해야 하니 public으로 선언)
    public void SaveCurrentDresses(){
        PlayerPrefs.SetInt("hair",currentHairNumber); //key, value값
        PlayerPrefs.SetInt("head",currentHeadNumber);
        PlayerPrefs.SetInt("top",currentTopNumber);
        PlayerPrefs.SetInt("leg",currentLegNumber);
        //저장 하면 Room으로 간다
        SceneManager.LoadScene("room");
    }
    public void BackAdornment(){
        SceneManager.LoadScene("Change");
    }
    //데이터 불러오기 character script 밖에서 접근할 필요 없으니 그냥 선언
    void LoadSavedDresses(){
        currentHairNumber = PlayerPrefs.GetInt("hair");
        currentHeadNumber = PlayerPrefs.GetInt("head");
        currentTopNumber = PlayerPrefs.GetInt("top");
        currentLegNumber = PlayerPrefs.GetInt("leg");
    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
