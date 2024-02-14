using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalScript : MonoBehaviour
{
    public void ConfirmBTN()
    {

    }

    public void CancelBTN()
    {
        Destroy(this.gameObject);
    }
}
