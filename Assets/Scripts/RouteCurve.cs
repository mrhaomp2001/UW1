using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteCurve : MonoBehaviour
{
    [SerializeField] private bool isDraw;
    
    [SerializeField] private List<Transform> controlPoints;

    private Vector2 gizmosPosition;

    private void Start()
    {
        isDraw = false;
    }

    private void OnDrawGizmos()
    {
        if(isDraw)
        {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                gizmosPosition = Mathf.Pow(1 - t, 3) * controlPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * controlPoints[2].position + Mathf.Pow(t, 3) * controlPoints[3].position;
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(gizmosPosition, 0.1f);
            }

            Gizmos.color = Color.white;
            Gizmos.DrawLine(new Vector2(controlPoints[0].position.x, controlPoints[0].position.y), new Vector2(controlPoints[1].position.x, controlPoints[1].position.y));
            Gizmos.DrawLine(new Vector2(controlPoints[2].position.x, controlPoints[2].position.y), new Vector2(controlPoints[3].position.x, controlPoints[3].position.y));
        }
    }
}
