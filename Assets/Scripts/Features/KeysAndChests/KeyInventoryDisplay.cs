using UnityEngine;
using UnityEngine.UI;

namespace Dots.Feature.KeyAndChest.Key.Display
{
    public class KeyInventoryDisplay : MonoBehaviour
    {
        [SerializeField] Image[] keysPlaceholder;
        [SerializeField] Sprite collectedKeySprite;
        [SerializeField] Sprite notCollectedKeySprite;

        void OnEnable()
        {
            DestroyingKeyLogic.OnKeyCollected += ChangeKeysPlaceholdersLooks;
        }

        void Awake()
        {
            if (DestroyingKeyLogic.TotalKeys > 0)
            {
                for (int i = 0; i < keysPlaceholder.Length; ++i)
                {
                    if (i < DestroyingKeyLogic.TotalKeys)
                    {
                        keysPlaceholder[i].sprite = collectedKeySprite;
                    }
                }
            }
        }

        void ChangeKeysPlaceholdersLooks(int keysTotal)
        {
            for (int i = 0; i < keysPlaceholder.Length; ++i)
            {
                if (i < keysTotal)
                    keysPlaceholder[i].sprite = collectedKeySprite;
                else
                    keysPlaceholder[i].sprite = notCollectedKeySprite;
            }
        }


        void OnDisable()
        {
            DestroyingKeyLogic.OnKeyCollected -= ChangeKeysPlaceholdersLooks;
        }
    }
}