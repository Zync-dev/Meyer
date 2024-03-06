using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIModalScript : MonoBehaviour
{

    MeyerAI m_AI;

    public int id = 0;

    private void Start()
    {
        m_AI = GameObject.Find("GameManager").GetComponent<MeyerAI>();
    }

    public void OkBtnClick()
    {
        this.gameObject.GetComponentInChildren<Button>().interactable = false;

        StartCoroutine(DestroyThis());
    }

    public IEnumerator DestroyThis()
    {
        if(id == 0)
        {
            m_AI.NewAIModalInstance();
        }
        yield return new WaitForSeconds(0.75f);

        Animator animator = GetComponent<Animator>();

        animator.Play("ModalHideAnim");

        Destroy(this.gameObject);
    }
}
