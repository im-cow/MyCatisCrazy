using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

    //고양이가 움직이는 속도
    private float moveSpeed;

    //나로호 날아갈때 쓰는거
    //--------------------------------------------
    //로켓이미지
    public Sprite rocket;
    //기본이미지
    public Sprite idle;
    //로켓이 날아갈 속도.
    public float rockSpeed;

    //나로호가 저장되어있는 col_naro를 가져올 GameObject 변수
    public GameObject naro;
    //그 naro 오브젝트의 이미지를 관리하는 SpriteRenderer 변수
    public SpriteRenderer spr;
    ///------------------------------------------
    ///키의 입력을 막을때 사용
    private bool isKey = true;

    //점프상태인지 확인
    public bool _isJumping;

    //점프할때 점프력
    private float _jumpPower;   //점프력

    //캐릭터 점프 사운드 출력용    
    private AudioSource adCtrl;

    //점프사운드
    public AudioClip jumpSound;

    public int moveDir = 0;

    //rigidBody 는 물체의 물리를 가져올 수 있음.
    private Rigidbody2D rgd;

    //Awake = Start
    private void Awake()
    {
        _isJumping = false;
        _jumpPower = 300.0f;

        moveSpeed = 5.0f;

        rgd = GetComponent<Rigidbody2D>();
        adCtrl = GetComponent<AudioSource>();

        spr = naro.GetComponent<SpriteRenderer>();

    }

    public void Update()
    {
        if(SceneController.GetInstance().currentStage == 4)
            _jumpPower = 150.0f; 
        else
            _jumpPower = 300.0f;
    }

    public void Move()
    {
        if (isKey)
        {
            float x = 0;

            //오른쪽으로 이동
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //양수 방향으로 움직임
                x = moveSpeed * Time.deltaTime;
                moveDir = 1;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                //음수 방향으로 움직이므로 - moveSpeed로 지정
                x = -moveSpeed * Time.deltaTime;
                moveDir = -1;
            }
            else
                //moveDir은 내가 움직이는 방향의 음.양 확인
                moveDir = 0;

            transform.Translate(x, 0, 0);

            //위쪽 키를 눌렀을 시, 점프중이 아니면
            if (Input.GetKeyDown(KeyCode.UpArrow) && !_isJumping)
            {
                //나로호 쏘는게 아닐때
                if (SceneController.GetInstance().currentStage != 9)
                {
                    //점프 사운드 출력 (PlayOneShot은 사운드를 1회출력하는 함수)
                    adCtrl.PlayOneShot(jumpSound);

                    //점프함수로 이동
                    Jump();
                }
                else
                {
                    isKey = false;
                    
                    StartCoroutine(GoingRocket());
                }
            }
        }
    }
    void Jump()
    {
        _isJumping = true;

        //rigid 힘을 줘서 물체를 밀어내는 함 수
        rgd.AddForce(Vector2.up * _jumpPower);

    }
    IEnumerator GoingRocket()
    {
        //사운드를 rocketSound로 변경
        SceneController.GetInstance().soundManagerSource.clip = SceneController.GetInstance().rockSound;

        //사운드를 재생
        SceneController.GetInstance().soundManagerSource.Play();

        //rocket 이미지로 변경하라.
        spr.sprite = rocket;

        //gravity 중력을 0으로 만듬.
        rgd.gravityScale = 0;

        //카메라의 초기위치를 camPos에 저장.
        Vector3 camPos = SceneController.GetInstance().mainCam.transform.position;

        //캐릭터가 날아갈 최종위치 지정
        Vector3 endPos = new Vector3(transform.position.x, 20.0f, transform.position.z);
        //카메라가 도착할 최종 위치 지정

        Vector3 endCamPos = new Vector3(camPos.x, 20.0f, camPos.z);

        //나는 나로호가 20.0f의 위치에 도착하면 while을 종료한다.
        while (transform.position.y < 20f){
            //wait없이 계속 다시 반복한다.
            yield return null;

            //distance 
            float dist = Vector3.Distance(transform.position, endPos);
            //rocketSpeed만큼 카메라와 캐릭터를 움직인다.
            SceneController.GetInstance().mainCam.transform.position = Vector3.Lerp(SceneController.GetInstance().mainCam.transform.position, endCamPos, (Time.deltaTime / dist) * rockSpeed);
            transform.position = Vector3.Lerp(transform.position, endPos, (Time.deltaTime / dist) * rockSpeed);
        }

        //Zi존 나로호를 출력한다.
        //튜토리얼을 위한 Text는 Active를 다시 True로 바꾼다.
        SceneController.GetInstance().logText.gameObject.SetActive(true);
        //그리고 list.count = list의 크기를 가져옴.
        //텍스트 초기화
        SceneController.GetInstance().logText.text = SceneController.GetInstance().logs[SceneController.GetInstance().logs.Count - 2];
        //5초간 기다려라.
        yield return new WaitForSeconds(5.0f);
        //사운드를 다시 main Sound로 변경해줘라.
        SceneController.GetInstance().soundManagerSource.clip = SceneController.GetInstance().mainSound;
        //똑같이 다시 재생
        SceneController.GetInstance().soundManagerSource.Play();
        //로그를 다보여줬으니 다시 false로 숨김.
        SceneController.GetInstance().logText.gameObject.SetActive(false);

        //중력 다시 1로 초기화
        rgd.gravityScale = 1;
        //이미지를 다시 기본으로 변경
        spr.sprite = idle;
        //모든 변경을 되돌린 후 key입력을 활성화

        isKey = true;
        //카메라 위치를 다시 원위치
        SceneController.GetInstance().mainCam.transform.position = new Vector3(0, 1, -20);

        //캐릭터 위치를 다시 원위치
        transform.position = new Vector3(-7.5f, -0.78f, 0);
    }
}
