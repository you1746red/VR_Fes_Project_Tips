using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoard : MonoBehaviour
{
    public Action PushKey;

    [SerializeField] GameObject _videoPlane;

    public void Push()
    {
        this.PushKey?.Invoke();
    }

    public void ShowInformation(bool isVisible)
    {
        this._videoPlane.SetActive(isVisible);
    }
}
