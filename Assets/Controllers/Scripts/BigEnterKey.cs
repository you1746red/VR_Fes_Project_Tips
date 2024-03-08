using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BigEnterKey : MonoBehaviour
{
    public Action PushKey;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] GameObject _videoPlane;

    public void Push()
    {
        this.PushKey?.Invoke();
        if (this._audioSource != null)
        {
            this._audioSource.Play();
        }
    }

    public void ShowVideoPlane(bool isVisible)
    {
        this._videoPlane.SetActive(isVisible);
    }
}
