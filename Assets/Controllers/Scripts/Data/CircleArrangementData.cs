using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleArrangementData
{
    public CircleArrangementData(
        float distance,
        float heightDiff,
        float xAngle,
        float yAngle)
    {
        this.Distance = distance;
        this.HeightDiff = heightDiff;
        this.XAngle = xAngle;
        this.YAngle = yAngle;
    }

    public float Distance { get; }
    public float HeightDiff { get; private set; }
    public float XAngle { get; }
    public float YAngle { get; }

    public void SetHeightDiff(float y)
    {
        this.HeightDiff = y;
    }
}
