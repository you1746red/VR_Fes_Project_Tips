using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleSphere : MonoBehaviour
{
    const float ANGLE_TICK = 45;

    public Action<bool, int> AddCount;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] GameObject _videoPlane;

    private Transform _leftHandTransform;
    private Transform _rightHandTransform;
    private bool _isLeftSide = true;

    private bool _isOnRotate = false;
    private bool _waitRotate = false;
    private float _fixedAngle = 0f;

    #region Update
    void Update()
    {
        if (this._isOnRotate == false || this._waitRotate) return;

        // 手首のx軸をキューブのz軸に適用
        Quaternion handRotation = this.GetHandRotation();
        float angle = handRotation.eulerAngles.x;
        float newFixedAngle = this.GetFixedAngleZ(angle);
        this.transform.rotation = this.GetNewRotation(newFixedAngle);

        // 角度判定
        float diff = this._fixedAngle - newFixedAngle;
        if (diff == 0) return;
        StartCoroutine(this.WaitRotate());

        // 鳴動
        this._audioSource.Play();

        // 差分値に応じて通知
        int coefficient = this._isLeftSide ? -1 : 1;
        int count = diff > 0 ? coefficient : -1 * coefficient;
        this.AddCount?.Invoke(this._isLeftSide, count);
    }
    #endregion

    #region Initialize
    public void Initialize(
        bool isLeftSide,
        Transform leftHandTransform,
        Transform rightHandTransform)
    {
        this._isLeftSide = isLeftSide;
        this._leftHandTransform = leftHandTransform;
        this._rightHandTransform = rightHandTransform;
    }
    #endregion

    public void ShowInformation(bool isVisible)
    {
        this._videoPlane.SetActive(isVisible);
    }

    /// イベント呼び出し

    #region OnRotate
    public void OnRotate()
    {
        // 音を鳴らさないように更新（angle→フラグの順）
        Quaternion handRotation = this.GetHandRotation();
        this._fixedAngle = this.GetFixedAngleZ(handRotation.eulerAngles.x);
        this._isOnRotate = true;
    }
    #endregion

    #region OffRotate
    public void OffRotate()
    {
        this._isOnRotate = false;
    }
    #endregion

    //// ローカル

    #region WaitRotate（コルーチン）
    private IEnumerator WaitRotate()
    {
        this._waitRotate = true;
        yield return new WaitForSeconds(1f);

        this._waitRotate = false;
        yield return null;
    }
    #endregion

    #region GetHandRotation
    private Quaternion GetHandRotation()
    {
        Transform handTrans = this._isLeftSide ? this._leftHandTransform : this._rightHandTransform;
        return Quaternion.Euler(
                handTrans.rotation.eulerAngles.x,
                handTrans.rotation.eulerAngles.y,
                handTrans.rotation.eulerAngles.z);
    }
    #endregion

    #region GetFixedAngleZ
    private float GetFixedAngleZ(float angle)
    {
        float fixAngle = angle;
        if (angle < 0)
        {
            fixAngle += 360f;
        }
        else if (angle > 180)
        {
            fixAngle -= 360f;
        }

        // ±指定角度刻みに補正
        float newFixedAngle = (int)(fixAngle / ANGLE_TICK) * ANGLE_TICK;
        return newFixedAngle;
    }
    #endregion

    #region GetNewRotation
    private Quaternion GetNewRotation(float angle)
    {
        // Z軸を変更したQuaternionを取得
        return Quaternion.Euler(
                this.transform.rotation.eulerAngles.x,
                this.transform.rotation.eulerAngles.y,
                angle);
    }
    #endregion
}
