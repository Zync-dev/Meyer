using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameModalScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Remove();
    }

    public IEnumerator Remove()
    {
        yield return new WaitForSeconds(4.5f);
        Destroy(this.gameObject);
    }
}
