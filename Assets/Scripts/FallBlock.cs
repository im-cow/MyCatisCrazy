using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class FallBlock : MonoBehaviour {

    //부딪혔는가 확인하는 bool 변수
    bool isFall = false;

    //내가 블록을 이동시킬 위치.
    public Vector3 endPos;

    public float speed;
	// Update is called once per frame
	void Update ()
    {
		if(isFall)
        {
            float distance = Vector3.Distance(transform.localPosition, endPos);
            //선형보.간
            //Time.deltaTime / distance는 두 벡터간의 거리가 짧아지는 것을 비례해서 같은 비율을 가지기 위해서 사용
            transform.localPosition = Vector3.Lerp(transform.localPosition, endPos, (Time.deltaTime / distance) * speed);
        }
	}

    //충돌했을때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //플레이어랑 충돌했을때.
        if(collision.gameObject.CompareTag("Player"))
            //떨어져야한다는 isFall을 true로 변경
            isFall = true;
    }
}
