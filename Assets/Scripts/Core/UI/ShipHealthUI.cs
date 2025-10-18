using UnityEngine;
using VContainer;
using FTL.Core.Services;
using FTL.Core.Components;

namespace FTL.Core.UI
{
    public class ShipHealthUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private UnityEngine.UI.Slider healthBar;
        [SerializeField] private TMPro.TextMeshProUGUI healthText;

        [Inject] public IShipService ShipService { get; private set; }
        [Inject] public ICombatService CombatService { get; private set; }

        private ShipComponent playerShip;

        private void Start()
        {
            // Get player ship from combat service
            playerShip = CombatService.GetPlayerShip();
        }

        private void Update()
        {
            if (playerShip != null)
            {
                float health = ShipService.GetShipHealth(playerShip);
                float maxHealth = playerShip.MaxHealth;

                healthBar.value = health / maxHealth;
                healthText.text = $"{health:F0}/{maxHealth:F0}";
            }
        }
    }
}
