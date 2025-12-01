namespace EventosShared;

public static class Config
{
	//public static string ConnectionString = "Data Source=localhost\\LEO;Initial Catalog=ARQUITETURA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
	public static string ConnectionString = "Server=localhost;Database=ARQUITETURA;User=root;Password=sae;";
	public static string JwtSecretKey = "supersecretkeythatshouldbeverylongandcomplex";
}
