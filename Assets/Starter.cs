using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Starter : MonoBehaviour
{
    private void Start()
    {
        Game game = new Game();
        game.Start();
    }

}
