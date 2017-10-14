using System;
using System.Collections.Generic;
using UnityEngine.Assertions;
using DylanJay.Services;
using DylanJay.Player;

namespace DylanJay.Framework
{
	public class ServiceLocator 
	{
        private static bool instantiated = false;
        private static Dictionary<Type, IService> serviceDict = new Dictionary<Type, IService>();

        // Managers
        static public IMonoBehaviourManager monoBehaviourManager { get { return serviceDict[typeof(IMonoBehaviourManager)] as IMonoBehaviourManager; } }
        static public IInputManager inputManager { get { return serviceDict[typeof(IInputManager)] as IInputManager; } }
        
        // GameObjects
        static public IPlayer player { get { return serviceDict[typeof(IPlayer)] as IPlayer; } }

        public ServiceLocator()
        {
            // Ensure only one instance is made
            //Assert.IsFalse(instantiated);
            if (instantiated) return;
            instantiated = true;

            //Managers
            serviceDict.Add(typeof(IMonoBehaviourManager), new MonoBehaviourManager());
            serviceDict.Add(typeof(IInputManager), new InputManager());

            // GameObjects
            serviceDict.Add(typeof(IPlayer), new PlayerController());
        }
	}
}
