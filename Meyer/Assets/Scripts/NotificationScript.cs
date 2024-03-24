using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationScript : MonoBehaviour
{

    public TMP_Text Header;
    public TMP_Text Text;

    private void Start()
    {
        StartCoroutine(DeleteSelf());
    }

    public void Notify(string header, string text)
    {
        Header.text = header; Text.text = text;
    }

    public IEnumerator DeleteSelf()
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
}
