using System;
using System.Collections.Generic;
using UnityEngine;
using DylanJay.Services;
using DylanJay.Player;

namespace DylanJay.Framework
{
    public class DependencyLocator
    {
        private static DependencyLocator _instance;
        public static DependencyLocator instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DependencyLocator();
                }
                return _instance;
            }
        }

        // Managers
        private IMonoBehaviourManager _monoBehaviourManager;
        public IMonoBehaviourManager monoBehaviourManager
        {
            get
            {
                if (_monoBehaviourManager == null)
                {
                    _monoBehaviourManager = GameObject.FindGameObjectWithTag("Management").GetComponent<MonoBehaviourManager>();
                }
                return _monoBehaviourManager;
            }
        }

        private IInputManager _inputManager;
        public IInputManager inputManager
        {
            get
            {
                if (_inputManager == null)
                {
                    _inputManager = new InputManager();
                }
                return _inputManager;
            }
        }

        // GameObjects
        private IPlayer _player;
        public IPlayer player
        {
            get
            {
                if (_player == null)
                {
                    _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
                }
                return _player;
            }
        }
    }
}
