using UnityEngine;
using UnityEngine.UI;
using Dots.Feature.KeyAndChest.Key.Model;
using Dots.Feature.KeyAndChest.Chest.Tap;

namespace Dots.Feature.KeyAndChest.Key.Display
{
    public class KeyInventoryDisplayPresenter : MonoBehaviour
    {
        [SerializeField] Image[] keysPlaceholder;
        [SerializeField] Sprite collectedKeySprite;
        [SerializeField] Sprite notCollectedKeySprite;

        void OnEnable()
        {
            DestroyingKeyLogic.OnKeyCollected += ChangeKeysPlaceholdersLooks;
            TapOnChest.OnTapOnChest += ChangeKeysPlaceholdersLooksOnTapOnChest;
        }

        void Awake()
        {
            if (KeysModel.TotalKeys > 0)
            {
                for (int i = 0; i < keysPlaceholder.Length; ++i)
                {
                    if (i < KeysModel.TotalKeys)
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
                keysPlaceholder[i].sprite = i < keysTotal ? collectedKeySprite : notCollectedKeySprite;
            }
        }

        void ChangeKeysPlaceholdersLooksOnTapOnChest(int keysTotal)
        {
            for (int i = keysPlaceholder.Length; i > 0; i--)
            {
                if (keysPlaceholder[i - 1].sprite == notCollectedKeySprite)
                {
                    continue;
                }

                keysPlaceholder[i - 1].sprite = i == keysTotal ? notCollectedKeySprite : collectedKeySprite;
            }
        }

        void OnDisable()
        {
            DestroyingKeyLogic.OnKeyCollected -= ChangeKeysPlaceholdersLooks;
            TapOnChest.OnTapOnChest -= ChangeKeysPlaceholdersLooksOnTapOnChest;
        }
    }
}