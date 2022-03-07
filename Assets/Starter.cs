using UnityEngine;

namespace ExpressionParser
{
    public sealed class Starter : MonoBehaviour
    {
        private void Start()
        {
            Game game = new Game();
            game.Start();
        }

    }
}
