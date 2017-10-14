using UnityEngine;

namespace DylanJay.Services
{
    public class MonoBehaviourManager : IMonoBehaviourManager
    {
        private event UpdateDelegate updateEvent;
        private event UpdateDelegate fixedUpdateEvent;
        private event UpdateDelegate lateUpdateEvent;

        private float _deltaTime;
        float IMonoBehaviourManager.deltaTime
        {
            get
            {
                return _deltaTime;
            }
        }

        private float _time;
        float IMonoBehaviourManager.time
        {
            get
            {
                return _time;
            }
        }

        private void Update()
        {
            // Set cached time variables for all monobehaviours to use
            _deltaTime = Time.deltaTime;
            _time = Time.time;

            if (updateEvent != null)
            {
                updateEvent.Invoke();
            }
        }

        private void FixedUpdate()
        {
            if (fixedUpdateEvent != null)
            {
                fixedUpdateEvent.Invoke();
            }
        }

        private void LateUpdate()
        {
            if (lateUpdateEvent != null)
            {
                lateUpdateEvent.Invoke();
            }
        }

        public void AddUpdate(UpdateDelegate myUpdate, UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Update:
                    updateEvent += myUpdate;
                    break;
                case UpdateType.FixedUpdate:
                    fixedUpdateEvent += myUpdate;
                    break;
                case UpdateType.LateUpdate:
                    lateUpdateEvent += myUpdate;
                    break;
            }
        }

        public void RemoveUpdate(UpdateDelegate myUpdate, UpdateType type)
        {
            switch (type)
            {
                case UpdateType.Update:
                    updateEvent -= myUpdate;
                    break;
                case UpdateType.FixedUpdate:
                    fixedUpdateEvent -= myUpdate;
                    break;
                case UpdateType.LateUpdate:
                    lateUpdateEvent -= myUpdate;
                    break;
            }
        }

        public void SetUpdate(UpdateDelegate myUpdate, UpdateType type, bool subscribe)
        {
            switch (type)
            {
                case UpdateType.Update:
                    updateEvent = subscribe ? updateEvent + myUpdate : updateEvent - myUpdate;
                    break;
                case UpdateType.FixedUpdate:
                    fixedUpdateEvent = subscribe ? fixedUpdateEvent + myUpdate : fixedUpdateEvent - myUpdate;
                    break;
                case UpdateType.LateUpdate:
                    lateUpdateEvent = subscribe ? lateUpdateEvent + myUpdate : lateUpdateEvent - myUpdate;
                    break;
            }
        }
    }
}
