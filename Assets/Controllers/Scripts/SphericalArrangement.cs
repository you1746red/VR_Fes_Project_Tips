using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphericalArrangement : MonoBehaviour
{
    public Transform _zeroSphereTrans;

    [Tooltip("���E���S���灛m�̋���"), SerializeField] float _distance;
    [Tooltip("�I�u�W�F�N�g�z�u���㉺�}���x�ɐݒ�"), SerializeField, Range(-180, 180f)] public float _xAngle;
    [Tooltip("�I�u�W�F�N�g�z�u�����E�}���x�ɐݒ�"), SerializeField, Range(-180, 180f)] public float _yAngle;

    private Vector3 _targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3F;

    private void Update()
    {
        // ���W�i�X���[�Y�ړ��j
        this.transform.position = Vector3.SmoothDamp(
            this.transform.position,
            this._targetPosition,
            ref velocity,
            smoothTime);

        // �p�x
        this.transform.LookAt(WorldCenterSphere.Instance.transform);
    }

    #region SetParam
    public void SetParam(SphericalArrangementData data)
    {
        this._distance = data.Distance;
        this._xAngle = data.XAngle;
        this._yAngle = data.YAngle;
    }
    #endregion

    #region ChangeDistance
    public void ChangeDistance(float value)
    {
        this._distance = value;
        this.Deploy();
    }
    #endregion

    #region ChangeDistanceDiff
    public void ChangeDistanceDiff(float diff)
    {
        this._distance += diff;
        this._distance = Mathf.Max(0, this._distance);
        this.Deploy();
    }
    #endregion

    #region Deploy
    /// <summary>
    /// �I�u�W�F�N�g�����ʔz�u����<br />
    /// �Q�lURL�@�p�x�ϊ��Fhttps://ekulabo.com/mathf-deg-rad<br />
    /// �Q�lURL�A���ʍ��W�ϊ��Fhttps://www.nsnq.tech/entry/2019/08/25/200000<br />
    /// </summary>
    public void Deploy(bool isImmediate = false)
    {
        Transform centerTrans = WorldCenterSphere.Instance.transform;

        // �I�C���[�p�����W�A���p�����ʍ��W�v�Z
        float degAngleTheta = -1 * this._xAngle;
        float degAngleFai = this._yAngle + centerTrans.eulerAngles.y - 90f;
        float radAngleTheta = degAngleTheta * Mathf.Deg2Rad;
        float radAngleFai = degAngleFai * Mathf.Deg2Rad;
        float posX = this._distance * Mathf.Cos(radAngleTheta) * Mathf.Cos(radAngleFai);
        float posY = this._distance * -1 * Mathf.Sin(radAngleTheta);
        float posZ = this._distance * -1 * Mathf.Cos(radAngleTheta) * Mathf.Sin(radAngleFai);
        Vector3 newPosition = new Vector3(posX, posY, posZ) + centerTrans.position;
        if (isImmediate)
        {
            // �����ړ�
            this.transform.position = newPosition;
            this._targetPosition = newPosition;
        }
        else
        {
            this._targetPosition = newPosition;
        }

        // �p�x
        this.transform.LookAt(centerTrans);
    }
    #endregion

    // ���[�J��

    #region Deploy_shisaku�@�������đʖڂ������X�N���v�g
    private void Deploy_shisaku()
    {
        Transform centerTrans = WorldCenterSphere.Instance.transform;
        //��forward�����S��Y��0�x�iVector3.forward�j�������Ă��Ȃ���X�����������Ƃ��ɃY����

        Debug.Log($"@@@ centerTrans: {centerTrans.position}");

        // �p�x�v�Z
        Quaternion rotationX = Quaternion.AngleAxis(this._xAngle, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(this._yAngle, Vector3.up);
        //Quaternion rotation = rotationX * rotationY; //�����̏�������30�x���΂߂̉~��`���悤�ɂȂ�
        Quaternion rotation = rotationY * rotationX;

        // ���W
        //Vector3 positionDiff = rotation * centerTrans.forward * this._distance;
        //this.transform.position = centerTrans.position + positionDiff;

        Vector3 positionDiff = rotation * this._zeroSphereTrans.forward * this._distance;
        this.transform.position = this._zeroSphereTrans.position + positionDiff;

        // �p�x
        //this.transform.rotation = Quaternion.Euler(
        //    centerTrans.rotation.eulerAngles.x + this._xAngle,
        //    centerTrans.rotation.eulerAngles.y + this._yAngle,
        //    centerTrans.rotation.eulerAngles.z);

        //this.transform.LookAt(this._zeroSphere);
        //this.transform.LookAt(centerTrans);
    }
    #endregion
}
