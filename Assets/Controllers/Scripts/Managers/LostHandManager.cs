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
        // �eGameObject��ۑ�
        this._regularLeftParentPos = this._leftHand.GetComponent<Transform>().parent;
        this._regularRightParentPos = this._rightHand.GetComponent<Transform>().parent;
    }

    void Update()
    {
        // ����
        if (this._leftHand.GetComponent<OVRHand>().HandConfidence == OVRHand.TrackingConfidence.Low)
        {
            // ���X�g
            this._leftHand.transform.parent = null;
            this._leftHand.transform.position = this._previousLeftPos;
        }
        else
        {
            // ���o
            this._leftHand.transform.parent = this._regularLeftParentPos;
            this._leftHand.transform.localPosition = Vector3.zero;
            this._leftHand.transform.localRotation = Quaternion.identity;
            this._leftHand.transform.localScale = Vector3.one;
            this._previousLeftPos = this._leftHand.transform.position;
        }

        // �E��
        if (this._rightHand.GetComponent<OVRHand>().HandConfidence == OVRHand.TrackingConfidence.Low)
        {
            // ���X�g
            this._rightHand.transform.parent = null;
            this._rightHand.transform.position = this._previousRightPos;
        }
        else
        {
            // ���o
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
            // ���X�g
            hand.transform.parent = null;
        }
        else
        {
            // ���o
            hand.transform.parent = parentPos;
            hand.transform.localPosition = Vector3.zero;
            hand.transform.localRotation = Quaternion.identity;
            hand.transform.localScale = Vector3.one;
        }
    }
    #endregion
}
