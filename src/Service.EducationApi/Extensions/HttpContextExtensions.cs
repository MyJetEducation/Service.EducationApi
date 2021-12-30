using System;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using SimpleTrading.UserAgent;

namespace Service.EducationApi.Extensions
{
	public static class HttpContextExtensions
	{
		private const string AcceptLanguage = "Accept-Language";
		private const string DefaultAcceptLanguage = "en-US";
		private const string UserAgent = "User-Agent";

		public static string GetLanguageOrDefault(this HttpContext ctx)
		{
			try
			{
				return GetLanguage(ctx);
			}
			catch (Exception)
			{
				return GetDefaultThreeLetterIsoLanguageName();
			}
		}

		public static string GetLanguage(this HttpContext ctx)
		{
			string acceptLanguages = ctx.Request.Headers.ContainsKey(AcceptLanguage)
				? ctx.Request.Headers[AcceptLanguage].ToString()
				: string.Empty;

			return GetLangByAcceptLanguage(acceptLanguages);
		}

		private static string GetDefaultThreeLetterIsoLanguageName()
		{
			var defaultLanguage = new CultureInfo(DefaultAcceptLanguage, false);
			return defaultLanguage.ThreeLetterISOLanguageName.ToUpper();
		}

		private static string GetLangByAcceptLanguage(string acceptLanguages)
		{
			try
			{
				IOrderedEnumerable<StringWithQualityHeaderValue> languages = acceptLanguages.Split(',')
					.Select(StringWithQualityHeaderValue.Parse)
					.OrderByDescending(s => s.Quality.GetValueOrDefault(1));

				string mainLang = languages.First().Value;

				var options = new RequestCulture(mainLang);

				string result = options.Culture.ThreeLetterISOLanguageName.ToUpper();

				return result;
			}
			catch (Exception ex)
			{
				throw new CultureNotFoundException(ex.Message);
			}
		}

		public static string GetRowUserAgent(this HttpContext ctx)
		{
			if (ctx.Request.Headers.ContainsKey(UserAgent))
				return ctx.Request.Headers[UserAgent].ToString();

			return string.Empty;
		}

		public static bool IsMobile(this HttpContext ctx)
		{
			string userAgent = ctx.GetRowUserAgent();

			return userAgent.IsNativeClient();
		}

		public static DeviceInfo GetDeviceInfo(this HttpContext ctx)
		{
			string userAgentString = ctx.GetRowUserAgent();

			return userAgentString.ParseUserAgent();
		}
	}
}