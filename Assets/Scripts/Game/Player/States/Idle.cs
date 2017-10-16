using UnityEngine;
using DylanJay.Framework;

namespace DylanJay.Player
{
    [CreateAssetMenu(fileName = "Idle", menuName = "States/Player/Idle")]
    public class Idle : PlayerState, IState
    {
        public void Enter()
        {
            player.model.moveVector = Vector3.zero;
        }

        public void Exit()
        {

        }

        public void Update()
        {
            player.characterController.Move(player.model.moveVector);
        }
    }
}