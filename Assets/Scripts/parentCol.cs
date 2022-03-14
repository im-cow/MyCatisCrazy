using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentCol : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            //부모의 jumping을 다시 false로 변경시켜준다.
            this.transform.parent.GetComponent<CharacterMove>()._isJumping = false;
        }
    }
}
