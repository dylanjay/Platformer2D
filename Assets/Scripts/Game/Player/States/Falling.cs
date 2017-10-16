using UnityEngine;
using DylanJay.Framework;

namespace DylanJay.Player
{
    [CreateAssetMenu(fileName = "Falling", menuName = "States/Player/Falling")]
    public class Falling : PlayerState, IState
    {
        private float deltaTime;
        private Vector2 gravity;

        public void Enter()
        {
            gravity = Physics2D.gravity;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            deltaTime = monoBehaviourManager.deltaTime;

            player.model.moveVector += gravity;

            player.model.moveVector.x = input.move.x.value * player.model.moveSpeed;

            player.characterController.Move(player.model.moveVector * deltaTime);
        }
    }
}