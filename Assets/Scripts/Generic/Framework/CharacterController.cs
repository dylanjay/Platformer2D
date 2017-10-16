using UnityEngine;

namespace DylanJay.Framework
{
	public class CharacterController : MonoBehaviourWrapper 
	{
        #region internal types

        struct CharacterRaycastOrigins
        {
            public Vector3 topLeft;
            public Vector3 bottomRight;
            public Vector3 bottomLeft;
        }

        public class CharacterCollisionState2D
        {
            public bool right;
            public bool left;
            public bool above;
            public bool below;
            public bool becameGroundedThisFrame;
            public bool wasGroundedLastFrame;
            public bool movingDownSlope;
            public float slopeAngle;


            public bool hasCollision()
            {
                return below || right || left || above;
            }


            public void reset()
            {
                right = left = above = below = becameGroundedThisFrame = movingDownSlope = false;
                slopeAngle = 0f;
            }


            public override string ToString()
            {
                return string.Format("[CharacterCollisionState2D] r: {0}, l: {1}, a: {2}, b: {3}, movingDownSlope: {4}, angle: {5}, wasGroundedLastFrame: {6}, becameGroundedThisFrame: {7}",
                                     right, left, above, below, movingDownSlope, slopeAngle, wasGroundedLastFrame, becameGroundedThisFrame);
            }
        }

        #endregion


    }
}