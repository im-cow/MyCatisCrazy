using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeCol : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //성적과 플레이어의 충돌 검사
        if(collision.gameObject.CompareTag("Player"))
        {
            //만약 9스테이지가 아니면 
            if (SceneController.GetInstance().currentStage != 8)
                //먹었을때 없어져야함.
                Destroy(this.gameObject);
            else
            {
                //col06 오브젝트를 가져옴.
                GameObject obj = GameObject.Find("col06") as GameObject;

                //부딪혔을경우 성적의 Parent를 col06으로 지정
                this.transform.SetParent(obj.transform);
                //부딪힌 후 성적의 위치를 귀에 꽂히도록 보이게 위치 변경
                this.transform.localPosition = new Vector3(0.114f, 0.2790661f, 0);
            }
        }
    }
}
