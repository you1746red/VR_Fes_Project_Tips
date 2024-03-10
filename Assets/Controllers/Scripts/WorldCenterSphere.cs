using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WorldCenterSphere : MonoBehaviour
{
    public static WorldCenterSphere Instance;

    [SerializeField] Transform _cameraTrans;
    [SerializeField] PenetratePassthroughSphere _penetratePassthroughSphere;
    [SerializeField] string _ovrPlaneName;

    private float _floorHeight;

    public float FloorHeight { get => this._floorHeight; }

    #region Awake
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Initialize
    /// <summary>
    /// オブジェクトの初期処理を行う<br />
    /// ①カメラ位置に座標・角度修正<br />
    /// ②パススルーの壁を消す<br />
    /// ③床までの距離算出<br />
    /// </summary>
    public void Initialize()
    {
        // 初期位置・角度
        this.transform.position = this._cameraTrans.position;
        this.transform.rotation = Quaternion.Euler(0f, this._cameraTrans.rotation.eulerAngles.y, 0f);

        // パススルー壁消し（正面±45度、高さ＋0.5mで実行）
        this._penetratePassthroughSphere.PenetrateRange(this.transform, 45, 0.5f, this._ovrPlaneName);

        // 現在位置と床プレーン位置からY軸補正値を計算
        GameObject floorPlane = this.GetFloorPlaneRayTest();
        if (floorPlane == null) return;
        this._floorHeight = floorPlane.transform.position.y - this.transform.position.y;

        // 床にタグ設定
        floorPlane.tag = "Ground";
    }
    #endregion

    #region GetPositionOnFloor
    /// <summary>
    /// 現在座標から床上座標を取得する
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector3 GetPositionOnFloor(Vector3 position)
    {
        // Y座標更新（世界中心＋床までの差分＋補正0.01）
        var newPosition = new Vector3(
            position.x,
            this.transform.position.y + this._floorHeight + 0.01f,
            position.z);
        return newPosition;
    }
    #endregion

    // ローカル

    #region GetFloorPlaneRayTest
    /// <summary>
    /// 床オブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    private GameObject GetFloorPlaneRayTest()
    {
        Vector3 forward = (-1) * this.transform.up;
        Ray ray = new Ray(this.transform.position, forward);
        RaycastHit hit;

        int distance = 10;
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        // Rayが指定名のオブジェクトが衝突時は非アクティブ化
        if (Physics.Raycast(ray, out hit, distance))
        {
            Debug.Log("@@@ {hit.collider.name}");
            if (hit.collider.name.Equals(this._ovrPlaneName))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }
    #endregion
}
