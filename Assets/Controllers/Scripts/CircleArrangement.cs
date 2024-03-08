using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleArrangement : MonoBehaviour
{
    [Tooltip("���E���S���灛m�̋���"), SerializeField] float _distance;
    [Tooltip("���E���S���灛m�̍�������"), SerializeField] float _heightDiff;
    [Tooltip("�I�u�W�F�N�g�̑O��X����ݒ肷��l"), SerializeField, Range(-180, 180f)] float _xAngle;
    [Tooltip("�I�u�W�F�N�g�z�u�𐳖ʁ}���x�ɐݒ肷��l"), SerializeField, Range(-180, 180f)] float _yAngle;

    private Vector3 _targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3F;

    private void Update()
    {
        // ���W
        this.transform.position = Vector3.SmoothDamp(
            this.transform.position,
            this._targetPosition,
            ref velocity,
            smoothTime);
    }

    #region SetParam
    public void SetParam(CircleArrangementData data)
    {
        this._distance = data.Distance;
        this._heightDiff = data.HeightDiff;
        this._xAngle = data.XAngle;
        this._yAngle = data.YAngle;
    }
    #endregion

    #region GetParam
    public CircleArrangementData GetParam()
    {
        var data = new CircleArrangementData(
            this._distance,
            this._heightDiff,
            this._xAngle,
            this._yAngle);
        return data;
    }
    #endregion

    #region ChangeDistance
    public void ChangeDistance(float diff)
    {
        this._distance += diff;
        this._distance = Mathf.Max(0, this._distance);
        this.Deploy();
    }
    #endregion

    #region Deploy
    /// <summary>
    /// �I�u�W�F�N�g���~�z�u�{��������
    /// </summary>
    public void Deploy()
    {
        Transform centerTrans = WorldCenterSphere.Instance.transform;
        Vector3 cameraForward = centerTrans.forward;

        // ����
        Quaternion rotation = Quaternion.AngleAxis(this._yAngle, Vector3.up);
        Vector3 rotationDirection = rotation * cameraForward * this._distance;

        // ���W
        Vector3 newPosition = centerTrans.position + rotationDirection;
        Vector3 newPosition2 = new Vector3(
            newPosition.x,
            centerTrans.position.y + this._heightDiff,
            newPosition.z);
        this.transform.position = newPosition2;
        this._targetPosition = newPosition2;

        // �p�x
        this.transform.rotation = Quaternion.Euler(
            this._xAngle,
            centerTrans.rotation.eulerAngles.y + this._yAngle,
            this.transform.eulerAngles.z);
    }
    #endregion
}
