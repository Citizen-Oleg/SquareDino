using Event;
using JetBrains.Annotations;
using UnityEngine;

namespace Screen
{
    public class StartScreen : MonoBehaviour
    {
        [UsedImplicitly]
        public void StartGame()
        {
            EventStreams.UserInterface.Publish(new StartGameEvent());
            gameObject.SetActive(false);
        }
    }
}
