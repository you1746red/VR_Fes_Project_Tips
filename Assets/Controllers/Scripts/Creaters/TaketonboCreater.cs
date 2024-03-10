using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaketonboCreater : MonoBehaviour
{
    [SerializeField] GameObject _taketonboPrefab;
    [SerializeField] Transform _leftHandTransform;
    [SerializeField] Transform _rightHandTransform;

    private List<GameObject> _taketonbos = new List<GameObject>();
    private GameObject _taketonboOnHand;

    [HideInInspector] public bool IsActived { get; private set; }

    #region Update
    private void Update()
    {
        if (this.IsActived == false) return;

        if (this._taketonboOnHand == null)
        {
            // ç∂éËÇ∆âEéËÇÃãóó£(10cmà»ì‡)Ç≈éËçáÇÌÇπîªíË
            if (this.CheckHandDistance(0.1f) == false) return;

            // í|Ç∆ÇÒÇ⁄çÏê¨
            this.CreateTaketonbo();
        }
        else
        {
            // ç∂éËÇ∆í|Ç∆ÇÒÇ⁄ÇÃãóó£(20cmà»ì‡)Ç≈èäéùîªíË
            if (this.CheckTaketonboDistance(0.2f)) return;
            this._taketonboOnHand = null;
        }
    }
    #endregion

    #region ActivateTaketonboSystem
    public void ActivateTaketonboSystem()
    {
        this.IsActived = true;
    }
    #endregion

    #region NotActivateTaketonboSystem
    public void NotActivateTaketonboSystem()
    {
        this.IsActived = false;
    }
    #endregion

    #region DestroyInChildren
    public void DestroyInChildren()
    {
        this._taketonbos.ForEach(obj => Destroy(obj.gameObject));
        this._taketonbos.Clear();
        this.IsActived = false;
    }
    #endregion

    // ÉçÅ[ÉJÉã

    #region CheckTaketonboDistance
    private bool CheckTaketonboDistance(float threshold)
    {
        float distance = Vector3.Distance(
            this._taketonboOnHand.transform.position,
            this._leftHandTransform.position);
        bool isRange = distance < threshold;
        return isRange;
    }
    #endregion

    #region CheckHandDistance
    private bool CheckHandDistance(float threshold)
    {
        float distance = Vector3.Distance(
            this._leftHandTransform.position,
            this._rightHandTransform.position);
        bool isRange = distance < threshold;
        return isRange;
    }
    #endregion

    #region CreateTaketonbo
    private void CreateTaketonbo()
    {
        // íÜä‘ç¿ïW
        Vector3 position = Vector3.Lerp(
            this._leftHandTransform.position,
            this._rightHandTransform.position,
            0.5f);

        // ê∂ê¨
        GameObject obj = Instantiate(
            this._taketonboPrefab,
            position,
            Quaternion.identity,
            this.transform);
        obj.GetComponent<Taketonbo>().LeftHandTransform = this._leftHandTransform;
        obj.GetComponent<Taketonbo>().RightHandTransform = this._rightHandTransform;
        this._taketonboOnHand = obj;
        this._taketonbos.Add(obj);
    }
    #endregion
}
