using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterAnimCtrl : MonoBehaviour {

    private int currentIndex = 0;
    public List<GameObject> changeChracters = new List<GameObject>();
    // Use this for initialization
    void Awake () {

	}
    public void Update()
    {
        currentIndex = SceneController.GetInstance().currentStage;
    }

    //방향에 따라 이미지를 회전해주는 함수
    public void ChangeDir(int dir)
    {
        if (dir == 1)
            changeChracters[currentIndex].transform.rotation = new Quaternion(0, 0, 0, 0);
        else if (dir == -1)
            changeChracters[currentIndex].transform.rotation = new Quaternion(0, 0.5f, 0, 0);
    }

    //스테이지에 따라 충돌및 이미지 Object를 On/off
    public void ChangeAnim(int index)
    {
        //block 을 On/Off 한거랑 똑같이 이전 충돌 및 이미지용 오브젝트를 끈다.
        changeChracters[index-1].SetActive(false);
        //block 을 On/Off 한거랑 똑같이 현재 충돌 및 이미지용 오브젝트를 켠다.
        changeChracters[index].SetActive(true);

    }
}
