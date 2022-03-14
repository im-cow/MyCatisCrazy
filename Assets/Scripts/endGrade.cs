using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endGrade : MonoBehaviour {

    //항상 로그를 출력해주던 그 Text 가져옴.
    public Text uiText;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어랑 충돌시
        if(collision.gameObject.CompareTag("Player"))
        {
            //log UI를 보이게 함.
            uiText.gameObject.SetActive(true);
            //리스트 제일 마지막 text로 변경
            uiText.text = SceneController.GetInstance().logs[SceneController.GetInstance().logs.Count - 1];

            //Destroy로 성적 오브젝트를 삭제해준다
            Destroy(this.gameObject);
        }
    }
}
