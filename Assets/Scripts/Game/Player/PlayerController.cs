using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DylanJay.Framework;
using DylanJay.Extensions;
using DylanJay.Services;
using System;

namespace DylanJay.Player
{
    [RequireComponent(typeof(PlayerModel))]
    [RequireComponent(typeof(CharacterController2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviourWrapper, IPlayer
	{
        #region Variables

        // Model
        private PlayerModel model;
        PlayerModel IPlayer.model
        {
            get
            {
                return model;
            }
        }

        // State Machine
        public StateMachine stateMachine = new StateMachine();
        public Dictionary<Type, IState> typeToState = new Dictionary<Type, IState>();
        public Dictionary<IState, Type> stateToType = new Dictionary<IState, Type>();

        // Services
        private IMonoBehaviourManager monoBehaviourManager;
        private IInputManager input;

        // Components
        new private Rigidbody2D rigidbody;
        Rigidbody2D IPlayer.rigidbody
        {
            get
            {
                return rigidbody;
            }
        }

        private CharacterController2D characterController;
        CharacterController2D IPlayer.characterController
        {
            get
            {
                return characterController;
            }
        }

        private Animator animator;
        Animator IPlayer.animator
        {
            get
            {
                return animator;
            }
        }

        // Cached Variables
        //private float deltaTime;

        // Debugging Variables
        public string currentState;

        #endregion

        private void Start()
        {
            InitializeUpdateFlags(true, false, false);

            model = GetComponent<PlayerModel>();
            NullOrEmptyExtension.AssertNotNull(model);

            rigidbody = GetComponent<Rigidbody2D>();
            NullOrEmptyExtension.AssertNotNull(rigidbody);

            characterController = GetComponent<CharacterController2D>();
            NullOrEmptyExtension.AssertNotNull(characterController);

            animator = GetComponent<Animator>();
            NullOrEmptyExtension.AssertNotNull(animator);

            monoBehaviourManager = dependencyLocator.monoBehaviourManager;
            NullOrEmptyExtension.AssertNotNull(monoBehaviourManager);

            input = dependencyLocator.inputManager;
            NullOrEmptyExtension.AssertNotNull(input);

            for (int i = 0; i < model.states.Count; i++)
            {
                typeToState.Add(model.states[i].GetType(), ScriptableObject.CreateInstance(model.states[i].GetType()) as IState);
                stateToType.Add(typeToState[model.states[i].GetType()], model.states[i].GetType());
            }

            stateMachine.ChangeState(typeToState[typeof(Falling)]);
        }

        protected override void MyUpdate()
        {
            if (characterController.isGrounded)
            {
                if (input.jump.wasPressed)
                {
                    stateMachine.ChangeState(typeToState[typeof(Jumping)]);
                }
                else if (input.move.x.value == 0)
                {
                    stateMachine.ChangeState(typeToState[typeof(Idle)]);
                }
                else
                {
                    stateMachine.ChangeState(typeToState[typeof(Running)]);
                }
            }
            else if (!characterController.isGrounded && model.moveVector.y < 0f)
            {
                stateMachine.ChangeState(typeToState[typeof(Falling)]);
            }

            // TEMP Debug
            currentState = stateMachine.currentState.ToString();

            stateMachine.Update();
        }
    }
}
