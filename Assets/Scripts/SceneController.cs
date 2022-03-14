using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    private static SceneController instance;

    //사운드를 관리
    public GameObject soundManager;
    public AudioSource soundManagerSource;

    //
    public Camera mainCam;

    //
    public AudioClip mainSound;
    public AudioClip rockSound;

    //
    public GameObject character;
    public int currentStage;

    //stage 넘어갈때마다 text를 변경해줘야함.
    public Text logText;

    //스테이지마다 출력할 텍스트의 순서 List
    public List<string> logs = new List<string>();
    //스테이지 보여질 block의 list
    public List<GameObject> stageBlock = new List<GameObject>();

    //전역적으로 다른 스크립트에서 SceneController의 멤버를 쓰기위해 만든 Get함수
    public static SceneController GetInstance()
    {
        if(instance == null)
        {
            instance = GameObject.FindObjectOfType<SceneController>();
        }
        return instance;
    }

    private void Awake()
    {
        soundManagerSource = soundManager.GetComponent<AudioSource>();
        //스테이지 순서 초기화
        currentStage = 0;
    }

    public void SetCharacter()
    {
        character.GetComponent<CharacterAnimCtrl>().ChangeAnim(currentStage);
    }

    public void setText()
    {
        //튜토리얼 (5스테이지 이전) 에는 로그를 업데이트
        if (currentStage < 4)
        {
            logText.text = logs[currentStage];
        }
        //아니면 Text UI를 꺼라.
        else
            logText.gameObject.SetActive(false);
    }


    //
    public void SetOnBlock()
    {
        //1->2 1 전 스테이지에있던 object를 끈다.
        stageBlock[currentStage - 1].SetActive(false);

        //현재 스테이지의 오브젝트를 켠다.
        stageBlock[currentStage].SetActive(true);
    }
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.N))
        //{
        //    SceneController.GetInstance().currentStage += 1;
        //    SceneController.GetInstance().SetOnBlock();
        //    SceneController.GetInstance().SetCharacter();
        //    SceneController.GetInstance().setText();


        //    Vector3 newPos = new Vector3(-7.5f, -0.78f, 0);
        //    character.transform.position = newPos;
        //    SceneController.GetInstance().character.transform.position = newPos;
        //}

        //Reset 부분. 캐릭터의 
        if(Input.GetKeyDown(KeyCode.R))
        {
            Vector3 newPos = new Vector3(-7.5f, -0.78f, 0);

            if (SceneController.GetInstance().currentStage == 5)
            {
                newPos = new Vector3(-12f, -0.78f, 0);
            }
            character.transform.position = newPos;
        }
    }
}
