using System.Collections.Generic;
using UnityEngine;

public class UnitsRTSPositionsCalculator
{
    public List<Vector3> GetPositionListAround(Vector3 startPoint, float[] ringDistance, int[] ringPositionCount)
    {
        List<Vector3> postionList = new List<Vector3>();
        postionList.Add(startPoint);

        for (int i = 0; i < ringDistance.Length; i++)
        {
            postionList.AddRange(GetPositionListAround(startPoint, ringDistance[i], ringPositionCount[i]));
        }

        return postionList;
    }

    private List<Vector3> GetPositionListAround(Vector3 startPoint, float distance, int positionCount)
    {
        List<Vector3> postionList = new List<Vector3>();

        for (int i = 0; i < positionCount; i++)
        {
            float angle = i * (360f / positionCount);
            Vector3 direction = ApplyRotationToVector(new Vector3(1, 0), angle);
            Vector3 position = startPoint + direction * distance;
            postionList.Add(position);
        }

        return postionList;
    }

    private Vector3 ApplyRotationToVector(Vector3 vector, float angle)
    {
        return Quaternion.Euler(0, 0, angle) * vector;
    }
}
