using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    internal interface IDialogService
    {

        /// <summary>
        /// Shows a text in the game world with a window prefab.
        /// </summary>
        /// <param name="parent">The GameObject that gets the UI prefab as a child.</param>
        /// <param name="position">Position of the text.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="duration">The duration until the window closes itself. If null the window only closes by calling Close</param>
        /// <returns>The id of the window which is needed to close it later.</returns>
        public int ShowGameTextWindow(GameObject parent, Vector3 position, string text, int? duration = null);

        /// <summary>
        /// Closes the window with the specified id.
        /// </summary>
        /// <param name="id">The window to close.</param>
        public void CloseGameTextWindow(int id);
    }
}
