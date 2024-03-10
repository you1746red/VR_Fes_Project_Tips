using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�Ƃ�ڃN���X<br />
/// ���蒆���������ˁ��n�㗎�������C�t�T�C�N���Ƃ��Ď���<br />
/// </summary>
public class Taketonbo : MonoBehaviour
{
    [HideInInspector] public Transform LeftHandTransform;
    [HideInInspector] public Transform RightHandTransform;

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _shotSound;
    [SerializeField] AudioClip _dropSound;

    private Rigidbody _rigidbody;
    private BoxCollider _collider;

    private bool _isGrabbed = true;
    private bool _isFirstRelease = true;
    private bool _isGround;
    private bool _addForcing;
    private float _timer;
    private float _addForceTime;

    private Vector3 _leftHandPosition1;
    private Vector3 _leftHandPosition2;
    private Vector3 _rightHandPosition1;
    private Vector3 _rightHandPosition2;

    void Start()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._rigidbody.useGravity = false;
        this._collider = this.GetComponent<BoxCollider>();
        this._collider.enabled = false;
    }

    void Update()
    {
        if (this._isGround)
        {
            // ���n�㗎�����̏���
            // ��]��~
            this.transform.Rotate(Vector3.zero);
            return;
        }

        this._isGrabbed = this._isGrabbed && this.CheckHandDistance(0.1f);
        if (this._isGrabbed)
        {
            // ���蒆�̏���
            // ���W�𗼎蒆�Ԃɕ␳
            this.transform.position = Vector3.Lerp(
                this.LeftHandTransform.position,
                this.RightHandTransform.position,
                0.5f);

            // ��]�p�������ł��������ɕ␳�i�p�x�͒|�Ƃ�ڂ���������悤�ɒ����j
            Quaternion rot = Quaternion.Euler(0, -90, 85);
            this.transform.rotation = this.LeftHandTransform.rotation * rot;
        }
        else
        {
            // ���肩�痣�ꂽ��̏���
            this._rigidbody.useGravity = true;

            if (this._isFirstRelease)
            {
                // ���肩�痣�ꂽ����̏���
                this._isFirstRelease = false;

                // ���W�ω��L�^�����ˁi����������FixedUpdate�Łj
                StartCoroutine(this.RecordPosition(0.1f));
            }

            // ��]�i���x�x�N�g�����瓱�o�j
            var velocityPerDeltaTime = this._rigidbody.velocity.magnitude * Time.deltaTime * 1000;
            var velocity = Mathf.Clamp(velocityPerDeltaTime, 2f, 15f);
            Vector3 vector = new Vector3(0, velocity, 0) * (-1);
            this.transform.Rotate(vector);
        }
    }

    private void FixedUpdate()
    {
        if (this._isGround || this._addForcing == false)
        {
            this.ClearHandPos();
            return;
        }

        // ���������i���[�N���b�h�������瓱�o�j
        float distance = this.CalcEuclideanDistance();
        if (distance > 0.1f)
        {
            // �������Ԃ��Z�o
            this._audioSource.clip = this._shotSound;
            this._audioSource.Play();
            this.ClearHandPos();
            this._isGround = false;
            this._addForcing = true;
            this._addForceTime = distance * 2.5f;
        }

        this._timer += Time.deltaTime;
        if (this._timer < this._addForceTime)
        {
            // ��Ƃ̊���}���邽��
            if (this._timer > this._addForceTime / 2f)�@this._collider.enabled = true;
            this._rigidbody.AddForce(this.transform.up * 50, ForceMode.Acceleration);
        }
        else
        {
            this._addForcing = false;
        }
    }

    #region CalcEuclideanDistance
    private float CalcEuclideanDistance()
    {
        Vector3 leftMoveVector = this._leftHandPosition2 - this._leftHandPosition1;
        Vector3 rightMoveVector = this._rightHandPosition2 - this._rightHandPosition1;
        float distance = Vector3.Distance(leftMoveVector, rightMoveVector);
        return distance;
    }
    #endregion

    #region ClearHandPos
    private void ClearHandPos()
    {
        this._leftHandPosition1 = Vector3.zero;
        this._leftHandPosition2 = Vector3.zero;
        this._rightHandPosition1 = Vector3.zero;
        this._rightHandPosition2 = Vector3.zero;
    }
    #endregion

    // ���[�J��

    #region CheckHandDistance
    private bool CheckHandDistance(float threshold)
    {
        float distance = Vector3.Distance(
            this.LeftHandTransform.position,
            this.RightHandTransform.position);
        bool isRange = distance < threshold;
        return isRange;
    }
    #endregion

    //// Collision�C�x���g

    #region OnCollisionEnter
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            this._isGround = true;
            this._audioSource.clip = this._dropSound;
            this._audioSource.Play();
        }
    }
    #endregion

    /// �R���[�`��

    #region RecordPosition
    private IEnumerator RecordPosition(float waitTime)
    {
        this._leftHandPosition1 = this.LeftHandTransform.position;
        this._rightHandPosition1 = this.RightHandTransform.position;
        yield return new WaitForSeconds(waitTime);

        this._leftHandPosition2 = this.LeftHandTransform.position;
        this._rightHandPosition2 = this.RightHandTransform.position;
        this._addForcing = true;
        yield return null;
    }
    #endregion
}
