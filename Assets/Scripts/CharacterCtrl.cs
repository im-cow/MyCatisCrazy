using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour {

    private CharacterMove m_moveCtrl;
    private CharacterAnimCtrl m_animCtrl;

    //Move와 Animation Ctrl를 Update해주는 스크립트
    // Use this for initialization
    void Start ()
    {
        m_moveCtrl = GetComponent<CharacterMove>();
        m_animCtrl = GetComponent<CharacterAnimCtrl>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        m_moveCtrl.Move();
        m_animCtrl.ChangeDir(m_moveCtrl.moveDir);
	}
}
