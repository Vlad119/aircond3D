using EasyMobile;
using UnityEngine;

public class EasyMobileInitializer : MonoBehaviour
{
    void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }
}
