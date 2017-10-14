using DylanJay.Framework;

namespace DylanJay.Services
{
    public delegate void UpdateDelegate();

    public enum UpdateType { Update, FixedUpdate, LateUpdate }

    public interface IMonoBehaviourManager : IService
	{
		float deltaTime { get; }
        float time { get; }
        void AddUpdate(UpdateDelegate updateDelegate, UpdateType updateType);
        void RemoveUpdate(UpdateDelegate updateDelegate, UpdateType updateType);
        void SetUpdate(UpdateDelegate updateDelegate, UpdateType updateType, bool subscribe);
    }
}
