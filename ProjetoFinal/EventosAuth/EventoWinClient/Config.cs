namespace EventoWinClient;

public static class Config
{
	public static string AuthToken { get; set; } = string.Empty;

	public static HttpClient HttpClientAuth { get; } = new HttpClient
	{
		BaseAddress = new Uri("https://localhost:7015/")
	};

	public static HttpClient HttpClientEvento { get; } = new HttpClient
	{
		BaseAddress = new Uri("https://localhost:7153/")
	};

	public static HttpClient HttpClientCertificado { get; } = new HttpClient
	{
		BaseAddress = new Uri("http://localhost:8080/")
	};
}
