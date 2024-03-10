using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaketonboCreater1 : MonoBehaviour
{
    [SerializeField] GameObject _taketonboPrefab;
    [SerializeField] Transform _leftHandTrans;
    [SerializeField] Transform _rightHandTrans;
    [SerializeField] Transform _leftFingerTrans;
    [SerializeField] Transform _rightFingerTrans;

    private GameObject _taketonbo;

    void Update()
    {
        if (this._leftFingerTrans.gameObject.activeSelf == false)
        {
            //Debug.Log("leftLost");
            return;
        }

        if (this._rightFingerTrans.gameObject.activeSelf == false)
        {
            //Debug.Log("rightLost");
            return;
        }

        Vector3 midLeft = (this._leftHandTrans.position + this._leftFingerTrans.position) / 2f;
        Vector3 midRight = (this._rightHandTrans.position + this._rightFingerTrans.position) / 2f;

        float diff = Vector3.Distance(midLeft, midRight);
        bool isNearly = diff < 0.1f;
        if (isNearly)
        {
            if (this._taketonbo != null)
            {
                float diff2 = Vector3.Distance(midLeft, this._taketonbo.transform.position);
                if (diff2 < 0.6f)
                {
                    return;
                }
            }

            Vector3 midPos = (midLeft + midRight) / 2f;
            this._taketonbo = Instantiate(
                this._taketonboPrefab,
                midPos,
                Quaternion.identity,
                null);
        }
    }
}
