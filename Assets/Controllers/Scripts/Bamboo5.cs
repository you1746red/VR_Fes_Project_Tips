using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo5 : MonoBehaviour
{
    [SerializeField] Transform _bambooModel;
    [SerializeField] AudioSource _sound;
    [SerializeField] Transform _leftFingerTrans;
    [SerializeField] Transform _rightFingerTrans;

    private bool _leftHandGrabbing = false;
    private bool _rightHandGrabbing = false;
    private bool _isGround;
    private bool _addForcing;
    private float _timer;
    private float _addForceTime;

    private GameObject _leftHandObject = null;
    private GameObject _rightHandObject = null;
    private CapsuleCollider _capsuleCollider;
    private Rigidbody _rigidbody;

    private Vector3 _leftPos1;
    private Vector3 _leftPos2;
    private Vector3 _rightPos1;
    private Vector3 _rightPos2;

    void Start()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._capsuleCollider = this.GetComponent<CapsuleCollider>();
        this._capsuleCollider.enabled = true;
    }

    void Update()
    {
        // ◆テスト発射機能
        this.TestKeyDown();

        if (this._leftHandGrabbing && this._rightHandGrabbing)
        {
            Debug.Log("aaa");
            this._capsuleCollider.enabled = false;
            var leftHandTrans = this._leftHandObject.transform;
            var rightHandTrans = this._rightHandObject.transform;

            // 両手中間座標に補正
            Vector3 midLeft = (leftHandTrans.position + this._leftFingerTrans.position) / 2f;
            Vector3 midRight = (rightHandTrans.position + this._rightFingerTrans.position) / 2f;
            Vector3 midPos = (midLeft + midRight) / 2f;
            this.transform.position = midPos;

            // 左手角度に補正
            //Quaternion rot = Quaternion.Euler(0, 0, -100);
            Quaternion rot = Quaternion.Euler(0, -90, -85);
            this.transform.rotation = leftHandTrans.rotation * rot;

            // キャッチ中は回転なし（地面接地OFF）
            this._isGround = false;
            this.transform.Rotate(Vector3.zero);
            return;
        }

        if (this._isGround)
        {
            // 地面接触中は回転なし
            this.transform.Rotate(Vector3.zero);
            return;
        }

        // 回転処理（速度ベクトルから導出）
        var velocityPerDeltaTime = this._rigidbody.velocity.magnitude * Time.deltaTime * 1000;
        //Debug.Log("★Mag：" + this._rigidbody.velocity.magnitude + "　★Velo：" + velocityPerDeltaTime);
        var velocity = Mathf.Clamp(velocityPerDeltaTime, 2f, 15f);
        Vector3 vector = new Vector3(0, velocity, 0) * (-1);
        this.transform.Rotate(vector);
    }

    private void FixedUpdate()
    {
        if (this._leftHandGrabbing && this._rightHandGrabbing)
        {
            this.ClearHandPos();
            return;
        }

        if (this._isGround)
        {
            this.ClearHandPos();
            return;
        }

        // 加速処理（ユークリッド距離から導出）
        float distance = this.CalcEuclideanDistance();
        //Debug.Log("★--------------------★" + distance);
        if (distance > 0.1f)
        {
            this._sound.Play();
            this.ClearHandPos();
            var force = Mathf.Clamp(distance * 800, 0, 10000);
            //Debug.Log("★--------------------★Force★：" + force);
            //this._rigidbody.AddForce(this.transform.up * force, ForceMode.Acceleration);
            this._isGround = false;
            this._addForcing = true;
            this._addForceTime = distance * 2.5f;
        }

        if (this._addForcing)
        {
            Debug.Log(distance);
            this._timer += Time.deltaTime;
            if (this._timer < this._addForceTime)
            {
                this._rigidbody.AddForce(this.transform.up * 50, ForceMode.Acceleration);
            }
            else
            {
                this._addForcing = false;
            }
        }
    }

    #region TestKeyDown
    private void TestKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this._isGround = false;
            this._leftPos1 = new Vector3(0, 0, 0);
            this._leftPos2 = new Vector3(0, 0, 2);
            this._rightPos1 = new Vector3(0, 0, 0);
            this._rightPos2 = new Vector3(0, 0, -2);
        }
    }
    #endregion

    #region CalcEuclideanDistance
    private float CalcEuclideanDistance()
    {
        Vector3 leftMoveVector = this._leftPos2 - this._leftPos1;
        Vector3 rightMoveVector = this._rightPos2 - this._rightPos1;
        float distance = Vector3.Distance(leftMoveVector, rightMoveVector);
        return distance;
    }
    #endregion

    #region ClearHandPos
    private void ClearHandPos()
    {
        this._leftPos1 = Vector3.zero;
        this._leftPos2 = Vector3.zero;
        this._rightPos1 = Vector3.zero;
        this._rightPos2 = Vector3.zero;
    }
    #endregion

    //// Triggerイベント

    #region OnTriggerEnter
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            this._leftHandGrabbing = true;
            this._leftHandObject = other.gameObject;
        }
        else if (other.CompareTag("RightHand"))
        {
            this._rightHandGrabbing = true;
            this._rightHandObject = other.gameObject;
        }
        else if (other.CompareTag("Ground"))
        {
            this._isGround = true;
            this._sound.Play();
        }
    }
    #endregion

    #region OnTriggerExit
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _leftHandObject)
        {
            this._capsuleCollider.enabled = true;
            this._leftHandGrabbing = false;
            StartCoroutine("RecordLeftPosition");
        }
        else if (other.gameObject == _rightHandObject)
        {
            this._capsuleCollider.enabled = true;
            this._rightHandGrabbing = false;
            StartCoroutine("RecordRightPosition");
        }
    }
    #endregion

    /// コルーチン

    #region RecordLeftPosition
    private IEnumerator RecordLeftPosition()
    {
        this._leftPos1 = this._leftHandObject.transform.localPosition;
        yield return new WaitForSeconds(0.1f);

        this._leftPos2 = this._leftHandObject.transform.localPosition;
        this._leftHandObject = null;
        yield return null;
    }
    #endregion

    #region RecordRightPosition
    private IEnumerator RecordRightPosition()
    {
        this._rightPos1 = this._rightHandObject.transform.localPosition;
        yield return new WaitForSeconds(0.1f);

        this._rightPos2 = this._rightHandObject.transform.localPosition;
        this._rightHandObject = null;
        yield return null;
    }
    #endregion
}
