using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(LineRenderer))]
public class Bezier : MonoBehaviour
{
    public Transform[] controlPoints;
    public LineRenderer lineRenderer;

    [SerializeField] private int curveCount = 0;
    [SerializeField] private int layerOrder = 0;
    [SerializeField] private int SEGMENT_COUNT = 50;


    void Start()
    {
        if (!lineRenderer)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        lineRenderer.sortingLayerID = layerOrder;
        curveCount = (int)controlPoints.Length / 3;
    }

    void Update()
    {
        DrawCurve();

    }

    void DrawCurve()
    {
        for (int j = 0; j < curveCount; j++)
        {
            for (int i = 1; i <= SEGMENT_COUNT; i++)
            {
                float t = i / (float)SEGMENT_COUNT;
                int nodeIndex = j * 3;
                Vector3 pixel = CalculateCubicBezierPoint(t, controlPoints[nodeIndex].position, controlPoints[nodeIndex + 1].position, controlPoints[nodeIndex + 2].position, controlPoints[nodeIndex + 3].position);
                //   lineRenderer.SetVertexCount(();
                // lineRenderer.AddPosition(new Vector3(0, 0, 0)); //Para TrailRenderer
                lineRenderer.positionCount = (j * SEGMENT_COUNT) + i;
                lineRenderer.SetPosition((j * SEGMENT_COUNT) + (i - 1), pixel);

             //   GameObject obj = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cylinder), lineRenderer.GetPosition((j * SEGMENT_COUNT) + (i - 1)), Quaternion.identity * new Quaternion(0,0,-1,0));
                //  obj.transform.localScale = new Vector3(0.5,)
            }

        }
    }

    Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}