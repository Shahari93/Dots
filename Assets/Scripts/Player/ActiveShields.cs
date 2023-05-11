using UnityEngine;
using Dots.Utils.Interaction;
using Dots.GamePlay.Powerups.Shield;

public class ActiveShields : MonoBehaviour
{
    [SerializeField] GameObject[] shields;

    private static bool areShieldsActive;
    public static bool AreShieldsActive
    {
        get
        {
            return areShieldsActive;
        }
        set
        {
            areShieldsActive = value;
        }
    }

    private void OnEnable()
    {
        ShieldPowerup.OnCollectedShieldPowerup += EnableShieldsVisual;
    }

    private void Awake()
    {
        areShieldsActive = false;
        foreach (GameObject shield in shields)
        {
            shield.SetActive(false);
        }
    }

    private void EnableShieldsVisual(bool isShieldOn)
    {
        areShieldsActive = isShieldOn;
        foreach (GameObject shield in shields)
        {
            shield.SetActive(isShieldOn);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractableObjects interactable))
        {
            interactable.BehaveWhenInteractWithPlayer();
        }
    }

    private void OnDisable()
    {
        ShieldPowerup.OnCollectedShieldPowerup -= EnableShieldsVisual;
    }

}