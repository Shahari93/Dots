using UnityEngine;

namespace Dots.Utils.Contain
{
	public class ContainObjects : MonoBehaviour
	{
        void Awake()
        {
            PolygonCollider2D polygon = GetComponent<PolygonCollider2D>();
            Vector2[] points = polygon.points;
            EdgeCollider2D edge = gameObject.AddComponent<EdgeCollider2D>();
            edge.points = points;
            Destroy(polygon);
        }
    } 
}