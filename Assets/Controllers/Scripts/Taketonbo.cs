using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 竹とんぼクラス<br />
/// ※手中生成→発射→地上落下をライフサイクルとして実装<br />
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
            // ★地上落下時の処理
            // 回転停止
            this.transform.Rotate(Vector3.zero);
            return;
        }

        this._isGrabbed = this._isGrabbed && this.CheckHandDistance(0.1f);
        if (this._isGrabbed)
        {
            // ★手中の処理
            // 座標を両手中間に補正
            this.transform.position = Vector3.Lerp(
                this.LeftHandTransform.position,
                this.RightHandTransform.position,
                0.5f);

            // 回転角を左手基準でいい感じに補正（角度は竹とんぼが上を向くように調整）
            Quaternion rot = Quaternion.Euler(0, -90, 85);
            this.transform.rotation = this.LeftHandTransform.rotation * rot;
        }
        else
        {
            // ★手から離れた後の処理
            this._rigidbody.useGravity = true;

            if (this._isFirstRelease)
            {
                // ★手から離れた初回の処理
                this._isFirstRelease = false;

                // 座標変化記録→発射（加速処理はFixedUpdateで）
                StartCoroutine(this.RecordPosition(0.1f));
            }

            // 回転（速度ベクトルから導出）
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

        // 加速処理（ユークリッド距離から導出）
        float distance = this.CalcEuclideanDistance();
        if (distance > 0.1f)
        {
            // 加速時間を算出
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
            // 手との干渉を抑えるため
            if (this._timer > this._addForceTime / 2f)　this._collider.enabled = true;
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

    // ローカル

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

    //// Collisionイベント

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

    /// コルーチン

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
