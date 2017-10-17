using UnityEngine;
using DylanJay.Framework;

namespace DylanJay.Player
{
    [CreateAssetMenu(fileName = "Falling", menuName = "States/Player/Falling")]
    public class Falling : PlayerState, IState
    {
        private float deltaTime;
        private Vector2 gravity;
        private float velocity;

        public void Enter()
        {
            velocity = player.model.moveVector.y;
            gravity = Physics2D.gravity;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            deltaTime = monoBehaviourManager.deltaTime;

            player.model.moveVector.x = input.move.x.value * player.model.moveSpeed * deltaTime;

            player.model.moveVector.y = velocity * deltaTime + 0.5f * gravity.y * deltaTime * deltaTime;

            velocity += gravity.y * deltaTime;

            player.characterController.Move(player.model.moveVector);
        }
    }
}