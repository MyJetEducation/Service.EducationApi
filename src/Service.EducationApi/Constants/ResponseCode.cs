namespace Service.EducationApi.Constants
{
	public class ResponseCode
	{
		public const int Ok = 0;

		public const int Fail = -1;

		public const int UserNotFound = -2;

		public const int NoRequestData = -3;

		public const int NoResponseData = -4;

		public const int UserAlreadyExists = -12;
	}

	public class LoginRequestValidationResponseCode
	{
		public const int PasswordContainsNoDigit = -5;

		public const int PasswordContainsNoLetter = -6;

		public const int PasswordInvalidLength = -10;

		public const int NotValidEmail = -11;
	}
}