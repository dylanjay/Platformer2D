using UnityEngine;
using DylanJay.Framework;

namespace DylanJay.Player
{
    [CreateAssetMenu(fileName = "Running", menuName = "States/Player/Running")]
    public class Running : PlayerState, IState
    {
        private float deltaTime;

        public void Enter()
        {
            
        }

        public void Exit()
        {

        }

        public void Update()
        {
            deltaTime = monoBehaviourManager.deltaTime;

            player.model.moveVector.x = input.move.x.value * player.model.moveSpeed;

            player.characterController.Move(player.model.moveVector * deltaTime);
        }
    }
}