using UnityEngine;
using DylanJay.Services;

namespace DylanJay.Framework
{
    public class MonoBehaviourWrapper : MonoBehaviour
    {
        protected DependencyLocator dependencyLocator;

        private IMonoBehaviourManager monoBehaviourManager;
        
        private bool callUpdate = false;
        private bool callFixedUpdate = false;
        private bool callLateUpdate = false;

        protected virtual void MyUpdate() { }
        protected virtual void MyFixedUpdate() { }
        protected virtual void MyLateUpdate() { }

        public delegate void Task();

        public void Invoke(Task task, float time)
        {
            Invoke(task.Method.Name, time);
        }

        public void StartCoroutine(Task task, object value)
        {
            StartCoroutine(task.Method.Name, value);
        }

        private void Awake()
        {
            dependencyLocator = DependencyLocator.instance;
            monoBehaviourManager = dependencyLocator.monoBehaviourManager;
        }

        protected void SetUpdateFlags(UpdateDelegate myUpdate, UpdateType updateType, bool active)
        {
            switch (updateType)
            {
                case UpdateType.FixedUpdate:
                    if (callFixedUpdate != active)
                    {
                        monoBehaviourManager.SetUpdate(myUpdate, UpdateType.FixedUpdate, active);
                        callFixedUpdate = active;
                    }
                    break;
                case UpdateType.LateUpdate:
                    if (callLateUpdate != active)
                    {
                        monoBehaviourManager.SetUpdate(myUpdate, UpdateType.LateUpdate, active);
                        callLateUpdate = active;
                    }
                    break;
                default:
                    if (callUpdate != active)
                    {
                        monoBehaviourManager.SetUpdate(myUpdate, UpdateType.Update, active);
                        callUpdate = active;
                    }
                    break;
            }
        }

        protected void InitializeUpdateFlags(bool callUpdate = false, bool callFixedUpdate = false, bool callLateUpdate = false)
        {
            if (callUpdate)
            {
                SetUpdateFlags(MyUpdate, UpdateType.Update, true);
            }
            if (callFixedUpdate)
            {
                SetUpdateFlags(MyFixedUpdate, UpdateType.FixedUpdate, true);
            }
            if (callLateUpdate)
            {
                SetUpdateFlags(MyLateUpdate, UpdateType.LateUpdate, true);
            }
        }
    }
}
