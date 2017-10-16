using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DylanJay.Framework;

namespace DylanJay.Player
{
	public class PlayerModel : MonoBehaviourWrapper 
	{
        // References
        public List<PlayerState> states;

        // Tweakables
        [Header("Stats")]
        public float moveSpeed = 8f;
        public float initialJumpVelocity = 5f;

        // Movement
        public Vector2 moveVector;
    }
}
