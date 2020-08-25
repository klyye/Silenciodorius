using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    ///     Handles the user interface while the game is running.
    /// TODO: depth counter
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class UIManager : MonoBehaviour
    {
        /// <summary>
        ///     The Pause menu gameobject.
        /// </summary>
        [SerializeField] private Image pauseMenu;

        private void Start()
        {
            PauseUI = false;
        }

        public bool PauseUI
        {
            set => pauseMenu.gameObject.SetActive(value);
        }
    }
}