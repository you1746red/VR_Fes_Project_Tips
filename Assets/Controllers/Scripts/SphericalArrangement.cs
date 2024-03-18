using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphericalArrangement : MonoBehaviour
{
    public Transform _zeroSphereTrans;

    [Tooltip("世界中心から○mの距離"), SerializeField] float _distance;
    [Tooltip("オブジェクト配置を上下±○度に設定"), SerializeField, Range(-180, 180f)] public float _xAngle;
    [Tooltip("オブジェクト配置を左右±○度に設定"), SerializeField, Range(-180, 180f)] public float _yAngle;

    private Vector3 _targetPosition;
    private Vector3 velocity = Vector3.zero;
    private float smoothTime = 0.3F;

    private void Update()
    {
        // 座標（スムーズ移動）
        this.transform.position = Vector3.SmoothDamp(
            this.transform.position,
            this._targetPosition,
            ref velocity,
            smoothTime);

        // 角度
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
    /// オブジェクトを球面配置する<br />
    /// 参考URL①角度変換：https://ekulabo.com/mathf-deg-rad<br />
    /// 参考URL②球面座標変換：https://www.nsnq.tech/entry/2019/08/25/200000<br />
    /// </summary>
    public void Deploy(bool isImmediate = false)
    {
        Transform centerTrans = WorldCenterSphere.Instance.transform;

        // オイラー角→ラジアン角→球面座標計算
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
            // 即時移動
            this.transform.position = newPosition;
            this._targetPosition = newPosition;
        }
        else
        {
            this._targetPosition = newPosition;
        }

        // 角度
        this.transform.LookAt(centerTrans);
    }
    #endregion

    // ローカル

    #region Deploy_shisaku　※試して駄目だったスクリプト
    private void Deploy_shisaku()
    {
        Transform centerTrans = WorldCenterSphere.Instance.transform;
        //★forwardが完全にY軸0度（Vector3.forward）を向いていないとX軸加味したときにズレる

        Debug.Log($"@@@ centerTrans: {centerTrans.position}");

        // 角度計算
        Quaternion rotationX = Quaternion.AngleAxis(this._xAngle, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(this._yAngle, Vector3.up);
        //Quaternion rotation = rotationX * rotationY; //★この順序だと30度が斜めの円を描くようになる
        Quaternion rotation = rotationY * rotationX;

        // 座標
        //Vector3 positionDiff = rotation * centerTrans.forward * this._distance;
        //this.transform.position = centerTrans.position + positionDiff;

        Vector3 positionDiff = rotation * this._zeroSphereTrans.forward * this._distance;
        this.transform.position = this._zeroSphereTrans.position + positionDiff;

        // 角度
        //this.transform.rotation = Quaternion.Euler(
        //    centerTrans.rotation.eulerAngles.x + this._xAngle,
        //    centerTrans.rotation.eulerAngles.y + this._yAngle,
        //    centerTrans.rotation.eulerAngles.z);

        //this.transform.LookAt(this._zeroSphere);
        //this.transform.LookAt(centerTrans);
    }
    #endregion
}
