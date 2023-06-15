using UnityEngine;
using DG.Tweening;
using Dots.Audio.Manager;
using System.Threading.Tasks;
using Dots.Utils.Interface.Spawnable;

namespace Dots.GamePlay.Dot
{
    public class SpawnableDots : MonoBehaviour, ISpawnableObjects
    {
        [SerializeField] protected Rigidbody2D rb2D;

        float dotSpeed;
        float randX;
        float randY;
        Vector2 direction;
        protected Vector3 startScale = new Vector3(0.71f, 0.71f, 0f);

        public float Speed { get => dotSpeed; set => dotSpeed = value; }
        public float RandX { get => RandX; set => randX = value; }
        public float RandY { get => RandY; set => randY = value; }
        public Vector2 Direction { get => direction; set => direction = value; }

        void OnEnable()
        {
            SetSpawnValues();
        }

        async void SetSpawnValues()
        {
            dotSpeed = Random.Range(85f, 95f);
            randX = Random.Range(-180, 181);
            randY = Random.Range(-180, 181);
            direction = new Vector2(randX, randY).normalized;
            transform.localScale = Vector3.zero;
            await Task.Delay(500);
            SetSpeedAndDirection();
        }

        public void SetSpeedAndDirection()
        {
            transform.DOScale(0.7f, 0.5f).OnComplete(() =>
            {
                if (gameObject.activeSelf)
                {
                    AudioManager.Instance.PlaySFX("DotCreated");
                }
                rb2D.velocity = dotSpeed * direction * Time.fixedDeltaTime;
            });
        }
    } 
}