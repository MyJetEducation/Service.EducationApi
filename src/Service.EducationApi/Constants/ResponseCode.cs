namespace Service.EducationApi.Constants
{
	public class ResponseCode
	{
		public const int Ok = 0;

		public const int Fail = -1;

		public const int UserNotFound = -2;

		public const int NoRequestData = -3;

		public const int NoResponseData = -4;
	}

	public class LoginRequestValidationResponseCode
	{
		public const int PasswordContainsNoDigit = -5;

		public const int PasswordContainsNoLetter = -6;

		public const int PasswordContainsNoSymbol = -7;

		public const int PasswordContainsNoUpper = -8;

		public const int PasswordContainsNoLower = -9;

		public const int PasswordTooShort = -10;

		public const int NotValidEmail = -11;
	}
}