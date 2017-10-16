using UnityEngine;
using DylanJay.Framework;
using DylanJay.Services;

namespace DylanJay.Player
{
    public class PlayerState : ScriptableObject
    {
        protected IPlayer player;
        protected IInputManager input;
        protected IMonoBehaviourManager monoBehaviourManager;

        private void Awake()
        {
            player = DependencyLocator.instance.player;
            input = DependencyLocator.instance.inputManager;
            monoBehaviourManager = DependencyLocator.instance.monoBehaviourManager;
        }
    }
}