using UnityEngine;
using DylanJay.Framework;

namespace DylanJay.Player
{
    public interface IPlayer : IDependency
    {
        PlayerModel model { get; }
        Rigidbody2D rigidbody { get; }
        CharacterController2D characterController { get; }
        Animator animator { get; }
	}
}
