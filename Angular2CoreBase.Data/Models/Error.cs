// NOTE: This error model is in 2 places, 
// please duplicate in Common/CommonModels as the DB entities are created from that location

using Angular2CoreBase.Data.Abstract;

namespace Angular2CoreBase.Data.Models
{
	public class Error : TrackedModelBase
	{
		public string Message { get; set; }

		public string ErrorLevel { get; set; }

		public string Source { get; set; }

		public string AdditionalInformation { get; set; }

		public string StackTrace { get; set; }
	}
}
