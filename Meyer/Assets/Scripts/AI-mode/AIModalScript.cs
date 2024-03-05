using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIModalScript : MonoBehaviour
{

    MeyerAI m_AI;

    private void Start()
    {
        m_AI = GameObject.Find("GameManager").GetComponent<MeyerAI>();
    }

    public void OkBtnClick()
    {
        Animator animator = GetComponent<Animator>();

        animator.Play("ModalHideAnim");

        StartCoroutine(DestroyThis());
    }

    public IEnumerator DestroyThis()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }
}
