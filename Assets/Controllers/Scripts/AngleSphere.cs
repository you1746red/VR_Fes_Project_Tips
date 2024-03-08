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

        // ����x�����L���[�u��z���ɓK�p
        Quaternion handRotation = this.GetHandRotation();
        float angle = handRotation.eulerAngles.x;
        float newFixedAngle = this.GetFixedAngleZ(angle);
        this.transform.rotation = this.GetNewRotation(newFixedAngle);

        // �p�x����
        float diff = this._fixedAngle - newFixedAngle;
        if (diff == 0) return;
        StartCoroutine(this.WaitRotate());

        // ��
        this._audioSource.Play();

        // �����l�ɉ����Ēʒm
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

    /// �C�x���g�Ăяo��

    #region OnRotate
    public void OnRotate()
    {
        // ����炳�Ȃ��悤�ɍX�V�iangle���t���O�̏��j
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

    //// ���[�J��

    #region WaitRotate�i�R���[�`���j
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

        // �}�w��p�x���݂ɕ␳
        float newFixedAngle = (int)(fixAngle / ANGLE_TICK) * ANGLE_TICK;
        return newFixedAngle;
    }
    #endregion

    #region GetNewRotation
    private Quaternion GetNewRotation(float angle)
    {
        // Z����ύX����Quaternion���擾
        return Quaternion.Euler(
                this.transform.rotation.eulerAngles.x,
                this.transform.rotation.eulerAngles.y,
                angle);
    }
    #endregion
}
