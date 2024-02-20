using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ModalScript : MonoBehaviour
{

    Meyer meyer;

    [SerializeField]
    TMP_Text NumView;

    int[] nums = { 32, 41, 42, 43, 51, 52, 53, 54, 61, 62, 63, 64, 65, 11, 22, 33, 44, 55, 66, 31, 21 };

    int currentIndex = 0;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        NumView.text = nums[currentIndex].ToString();

        print(nums.Length);
    }

    public void UpBtn()
    {
        if(currentIndex < nums.Length-1)
        {
            currentIndex++;
        }
        UpdateNumView();
    }

    public void DownBtn()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
        }
        UpdateNumView();
    }

    void UpdateNumView()
    {
        print("CURRENT INDEX: " + currentIndex.ToString());
        NumView.text = nums[currentIndex].ToString();
    }

    private void Awake()
    {
        meyer = GameObject.Find("GameManager").GetComponent<Meyer>();
    }

    public void ConfirmBTN()
    {
        meyer.BluffConfirmed(nums[currentIndex]);

        StartCoroutine(DeleteOBJ());
    }

    public void CancelBTN()
    {
        StartCoroutine(DeleteOBJ());
    }

    public IEnumerator DeleteOBJ()
    {
        animator.Play("InputModalExit");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
