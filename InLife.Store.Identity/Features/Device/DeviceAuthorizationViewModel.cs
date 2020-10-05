namespace InLife.Store.Identity.Features
{
	public class DeviceAuthorizationViewModel : ConsentViewModel
	{
		public string UserCode { get; set; }
		public bool ConfirmUserCode { get; set; }
	}
}