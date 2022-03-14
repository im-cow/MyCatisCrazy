using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

    public GameObject coll;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {


            //플레이어랑 충돌시 스테이지에 관한 모든처리
            SceneController.GetInstance().currentStage += 1;
            //다음 블럭을 켜고
            SceneController.GetInstance().SetOnBlock();

            //캐릭터의 Object를 다음 Object로 변경
            SceneController.GetInstance().SetCharacter();

            //다음 출력해야할 Text를 업데이트
            SceneController.GetInstance().setText();


            Vector3 newPos = new Vector3(-7.5f, -0.78f, 0);
            coll.transform.localPosition = new Vector3(-10.13f, 0, 0);

            //6스테이지는 몸뚱이가 길어서 왼쪽의 충돌 collider와
            //캐릭터의 위치를 좀 왼쪽으로 이동시켜준다.
            if (SceneController.GetInstance().currentStage == 5)
            {
                coll.transform.localPosition = new Vector3(-17f, 0, 0);

                newPos = new Vector3(-12f, -0.78f, 0);
            }
            //나머지는 정상적인 위치로 초기화해준다/
            other.transform.position = newPos;
            SceneController.GetInstance().character.transform.position = newPos;
            
        }
        
    }
}
