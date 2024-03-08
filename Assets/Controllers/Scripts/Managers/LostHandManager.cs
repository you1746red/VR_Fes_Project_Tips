using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostHandManager : MonoBehaviour
{
    [SerializeField] GameObject _leftHand;
    [SerializeField] GameObject _rightHand;

    private Transform _regularLeftParentPos;
    private Transform _regularRightParentPos;

    private Vector3 _previousLeftPos;
    private Vector3 _previousRightPos;

    private void Start()
    {
        // 親GameObjectを保存
        this._regularLeftParentPos = this._leftHand.GetComponent<Transform>().parent;
        this._regularRightParentPos = this._rightHand.GetComponent<Transform>().parent;
    }

    void Update()
    {
        // 左手
        if (this._leftHand.GetComponent<OVRHand>().HandConfidence == OVRHand.TrackingConfidence.Low)
        {
            // ロスト
            this._leftHand.transform.parent = null;
            this._leftHand.transform.position = this._previousLeftPos;
        }
        else
        {
            // 検出
            this._leftHand.transform.parent = this._regularLeftParentPos;
            this._leftHand.transform.localPosition = Vector3.zero;
            this._leftHand.transform.localRotation = Quaternion.identity;
            this._leftHand.transform.localScale = Vector3.one;
            this._previousLeftPos = this._leftHand.transform.position;
        }

        // 右手
        if (this._rightHand.GetComponent<OVRHand>().HandConfidence == OVRHand.TrackingConfidence.Low)
        {
            // ロスト
            this._rightHand.transform.parent = null;
            this._rightHand.transform.position = this._previousRightPos;
        }
        else
        {
            // 検出
            this._rightHand.transform.parent = this._regularRightParentPos;
            this._rightHand.transform.localPosition = Vector3.zero;
            this._rightHand.transform.localRotation = Quaternion.identity;
            this._rightHand.transform.localScale = Vector3.one;
            this._previousRightPos = this._rightHand.transform.position;
        }
    }

    #region EvacuationLostHand
    private void EvacuationLostHand(GameObject hand, Transform parentPos)
    {
        if (hand.GetComponent<OVRHand>().HandConfidence == OVRHand.TrackingConfidence.Low)
        {
            // ロスト
            hand.transform.parent = null;
        }
        else
        {
            // 検出
            hand.transform.parent = parentPos;
            hand.transform.localPosition = Vector3.zero;
            hand.transform.localRotation = Quaternion.identity;
            hand.transform.localScale = Vector3.one;
        }
    }
    #endregion
}
