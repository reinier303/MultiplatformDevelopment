using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnMobile : MonoBehaviour
{
    public bool disable;
    private void Awake()
    {
#if UNITY_ANDROID
        if(disable)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
#else
        if(disable)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
#endif
    }
}
