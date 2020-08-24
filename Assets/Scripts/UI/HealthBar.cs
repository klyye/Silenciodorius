using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    ///     A sliding bar representing the amount of health that a unit has remaining.
    ///     Must be parented by a Unit.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {

        /// <summary>
        ///     The Slider UI object the shows the health bar.
        /// </summary>
        public Slider slider;
        
        /// <summary>
        ///     The offset from the center of the sprite that the health bar should appear at.
        /// </summary>
        public Vector3 offset;
        
        /// <summary>
        ///     The color of the health in the health bar.
        /// </summary>
        public Color color;
        
        /// <summary>
        ///     Sets the healthbar to display a certain health value.
        /// </summary>
        /// <param name="health">The current health for the bar to display.</param>
        /// <param name="maxHealth">The maximum health that the health bar can hold.</param>
        public void SetHealth(float health, float maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = health;
            print($"My value is {slider.value} and my maximum value is {slider.maxValue}");
        }

        private void Update()
        {
            slider.transform.position = GameManager.cam.WorldToScreenPoint(transform.parent
                .position + offset);
        }

        private void Start()
        {
            slider.fillRect.GetComponentInChildren<Image>().color = color;
        }
    }
}