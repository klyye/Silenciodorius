using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    /// <summary>
    ///     Handles the user interface while the game is running.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class UIManager : MonoBehaviour
    {
        /// <summary>
        ///     The Pause menu gameobject.
        /// </summary>
        [SerializeField] private Image pauseMenu;

        /// <summary>
        ///     Displays how many floors the player has gone through.
        /// </summary>
        [SerializeField] private TMP_Text depthCounter;

        private void Start()
        {
            PauseUI = false;
        }

        public bool PauseUI
        {
            set => pauseMenu.gameObject.SetActive(value);
        }

        /// <summary>
        ///     Displays a new depth on the depth counter.
        /// </summary>
        /// <param name="depth">The new depth to display.</param>
        public void UpdateDepthCounter(int depth)
        {
            depthCounter.text = $"Floor: {depth}";
        }
    }
}