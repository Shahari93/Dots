using UnityEngine;

namespace Dots.Utilities.Spawn.CircleCircumference
{
    public class SpawnOnCircleCircumference
    {
        public static Vector3 SpawnObjectOnCircleCircumference(float radius)
        {
            float angle = Random.Range(0, Mathf.PI * 2);
            return new Vector3(Mathf.Sin(angle) * radius, Mathf.Cos(angle) * radius, 0);
        }
    } 
}