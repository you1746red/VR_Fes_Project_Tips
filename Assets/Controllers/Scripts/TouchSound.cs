using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSound : MonoBehaviour
{
    [SerializeField] AudioClip _audioClip;

    private void Start()
    {
        this.GetComponent<AudioSource>().clip = this._audioClip;
    }

    public void Touch()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
