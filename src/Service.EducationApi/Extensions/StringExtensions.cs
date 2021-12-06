using JetBrains.Annotations;

namespace Service.EducationApi.Extensions
{
	public static class StringExtensions
	{
		[ContractAnnotation("null=>true")]
		public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

		[ContractAnnotation("null=>true")]
		public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);
	}
}