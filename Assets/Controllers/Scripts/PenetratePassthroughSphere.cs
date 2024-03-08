using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PenetratePassthroughSphere : MonoBehaviour
{
    public void PenetrateRange(Transform centerTrans, int radius, float height, string ovrPlaneName)
    {
        // �����ʒu�i���E���S����uheight�v�㕔�j
        this.transform.position = centerTrans.position + new Vector3(0f, height, 0f);

        // �����p�x
        float angleY = centerTrans.rotation.eulerAngles.y;

        foreach (int index in Enumerable.Range(0, radius * 2))
        {
            float newRadiusY = -1 * radius + index;

            // �p�x����
            this.transform.rotation = Quaternion.Euler(
                this.transform.eulerAngles.x,
                angleY + newRadiusY,
                this.transform.eulerAngles.z);

            // ���C�ɐڐG���������ǂ��\��
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

        // Ray���w�薼�̃I�u�W�F�N�g���Փˎ��͔�A�N�e�B�u��
        if (Physics.Raycast(ray, out hit, distance))
        {
            // �p�X�X���[�ǌŒ薼�̂𔻒�
            if (hit.collider.name.Equals(ovrPlaneName))
            {
                hit.collider.gameObject.SetActive(false);
            }
        }
    }
}
