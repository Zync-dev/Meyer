using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameModalScript2 : MonoBehaviour
{

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(Remove());
    }

    public IEnumerator Remove()
    {
        yield return new WaitForSeconds(4f);
        animator.Play("NewGameModal2Exit");
;       yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
