using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalArrangementData
{
    public SphericalArrangementData(
        float distance,
        float xAngle,
        float yAngle)
    {
        this.Distance = distance;
        this.XAngle = xAngle;
        this.YAngle = yAngle;
    }

    public float Distance { get; }
    public float XAngle { get; }
    public float YAngle { get; }
}
