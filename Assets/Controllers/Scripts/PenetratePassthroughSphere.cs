using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PenetratePassthroughSphere : MonoBehaviour
{
    public void PenetrateRange(Transform centerTrans, int radius, float height, string ovrPlaneName)
    {
        // 初期位置（世界中心から「height」上部）
        this.transform.position = centerTrans.position + new Vector3(0f, height, 0f);

        // 初期角度
        float angleY = centerTrans.rotation.eulerAngles.y;

        foreach (int index in Enumerable.Range(0, radius * 2))
        {
            float newRadiusY = -1 * radius + index;

            // 角度調整
            this.transform.rotation = Quaternion.Euler(
                this.transform.eulerAngles.x,
                angleY + newRadiusY,
                this.transform.eulerAngles.z);

            // レイに接触した透明壁を非表示
            this.PenetratePassthrough(ovrPlaneName);
        }
    }

    private void PenetratePassthrough(string ovrPlaneName)
    {
        Vector3 forward = this.transform.forward;
        Ray ray = new Ray(this.transform.position, forward);
        RaycastHit hit;

        int distance = 10;
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        // Rayが指定名のオブジェクトが衝突時は非アクティブ化
        if (Physics.Raycast(ray, out hit, distance))
        {
            // パススルー壁固定名称を判定
            if (hit.collider.name.Equals(ovrPlaneName))
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }
}
