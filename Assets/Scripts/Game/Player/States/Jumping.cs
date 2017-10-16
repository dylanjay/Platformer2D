using UnityEngine;
using DylanJay.Framework;

namespace DylanJay.Player
{
    [CreateAssetMenu(fileName = "Jumping", menuName = "States/Player/Jumping")]
    public class Jumping : PlayerState, IState
    {
        private float deltaTime;
        private Vector2 gravity;

        public void Enter()
        {
            player.model.moveVector.y = player.model.initialJumpVelocity;
            //player.rigidbody.velocity = new Vector2(player.rigidbody.velocity.x, player.model.initialJumpVelocity);
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