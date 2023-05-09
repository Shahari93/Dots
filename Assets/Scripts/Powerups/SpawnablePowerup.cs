using UnityEngine;

namespace Dots.Utils.Spawnable
{
    public class SpawnablePowerup : MonoBehaviour, ISpawnableObjects
    {
        float randX;
        float randY;
        float spawnSpeed;
        Vector2 direction;

        [SerializeField] protected Rigidbody2D rb2D;

        public float Speed { get => spawnSpeed; set => spawnSpeed = value; }
        public float RandX { get => RandX; set => randX = value; }
        public float RandY { get => RandY; set => randY = value; }
        public Vector2 Direction { get => direction; set => direction = value; }

        void OnEnable()
        {
            SetSpawnValues();
        }

        void SetSpawnValues()
        {
            spawnSpeed = 80f;
            randX = Random.Range(-180, 181);
            randY = Random.Range(-180, 181);
            direction = new Vector2(randX, randY).normalized;
        }

        public void SetSpeedAndDirection()
        {
            rb2D.velocity = spawnSpeed * direction * Time.fixedDeltaTime;
        }

        void FixedUpdate()
        {
            SetSpeedAndDirection();
        }
    } 
}