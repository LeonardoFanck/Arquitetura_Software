using EventosShared.Model;

namespace EventoWinClient;

public static class Config
{
	public static string AuthToken { get; set; } = string.Empty;
	public static User User { get; set; } = null!;

	public static HttpClient HttpClientAuth { get; } = new HttpClient
	{
		BaseAddress = new Uri("http://177.44.248.41:8081/")
	};

	public static HttpClient HttpClientEvento { get; } = new HttpClient
	{
		BaseAddress = new Uri("http://177.44.248.41:8082/")
	};

	public static HttpClient HttpClientCertificado { get; } = new HttpClient
	{
		BaseAddress = new Uri("http://177.44.248.41:8080/")
	};

	public static bool Internet { get; set; } = true;

	public static async Task<bool> HasNetworkAsync()
	{
		return Internet;

		try
		{
			HttpClient client = new()
			{
				Timeout = TimeSpan.FromSeconds(3)
			};

			using var response = await client.GetAsync("https://www.google.com");

			return response.IsSuccessStatusCode;
		}
		catch
		{
			return false;
		}
	}
}
