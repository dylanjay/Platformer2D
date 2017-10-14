namespace DylanJay.Framework
{
	public class Locator 
	{
		public void Initialize()
        {
            _service = _nullService;
        }

        private IService _service;
        private INullService _nullService;
	}
}
