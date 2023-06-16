using UnityEngine;
using Dots.Utilities.Interface.Spawnable;

namespace Dots.Utilities.Spawnable
{
    /// <summary>
    /// This class is responsible of setting the values for each powerup when it spawn
    /// Like the direction and the speed of the powerup
    /// </summary>
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
        /// <summary>
        /// Setting values for the dot before it spawns 
        /// </summary>
        void SetSpawnValues()
        {
            spawnSpeed = 70;
            randX = Random.Range(-180, 181);
            randY = Random.Range(-180, 181);
            direction = new Vector2(randX, randY).normalized;
        }
        /// <summary>
        /// Setting the direction values for the dot after it spawns
        /// </summary>
        public void SetSpeedAndDirection()
        {
            rb2D.velocity = spawnSpeed * Time.fixedDeltaTime * direction;
        }

        void FixedUpdate()
        {
            SetSpeedAndDirection();
        }
    } 
}