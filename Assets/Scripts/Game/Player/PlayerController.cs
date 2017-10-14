using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DylanJay.Framework;
using DylanJay.Extensions;
using DylanJay.Services;

namespace DylanJay.Player
{
    [RequireComponent(typeof(PlayerModel))]
    [RequireComponent(typeof(CharacterController2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviourWrapper, IService
	{
        #region Variables

        // Model
        private PlayerModel model;

        // Services
        private IMonoBehaviourManager monoBehaviourManager;
        private IInputManager input;

        // Components
        private CharacterController2D characterController;
        private Animator animator;

        // Cached Variables
        private float deltaTime;

        // Movement
        private Vector2 moveVector;
        private Vector2 gravity;

        #endregion

        private void Start()
        {
            model = GetComponent<PlayerModel>();
            NullOrEmptyExtension.AssertNotNull(model);

            characterController = GetComponent<CharacterController2D>();
            NullOrEmptyExtension.AssertNotNull(characterController);

            animator = GetComponent<Animator>();
            NullOrEmptyExtension.AssertNotNull(animator);

            gravity = Physics2D.gravity;
        }

        private void Update()
        {
            deltaTime = monoBehaviourManager.deltaTime;

            moveVector.x = input.move.x.value * model.moveSpeed * deltaTime;

            //moveVector += gravity * deltaTime;

            characterController.move(moveVector);
        }
    }
}
