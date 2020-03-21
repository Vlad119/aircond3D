using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageScreen : MonoBehaviour
{
    public void Close()
    {
        AppManager.Instance.fon_message.SetActive(false);    
    }
}
