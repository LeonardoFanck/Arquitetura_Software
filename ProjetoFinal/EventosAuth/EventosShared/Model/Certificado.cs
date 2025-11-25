namespace EventosShared.Model;

public class Certificado
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public Guid EventoId { get; set; }

	public DateTime DataEmissao { get; set; }
	public string CodigoAutenticacao { get; set; } = string.Empty;

	public byte[] PdfArquivo { get; set; } = [];
}
