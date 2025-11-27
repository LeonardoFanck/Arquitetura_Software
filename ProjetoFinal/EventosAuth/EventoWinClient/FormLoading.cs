namespace EventoWinClient;

public partial class FormLoading : Form
{
	public FormLoading()
	{
		InitializeComponent();

		ControlBox = false;
		FormBorderStyle = FormBorderStyle.None;
		StartPosition = FormStartPosition.CenterScreen;
		TopMost = true;

		Load += FormLoading_Load;
	}

	private async void FormLoading_Load(object? sender, EventArgs e)
	{
		BringToFront();
		Focus();

		while (!IsDisposed)
		{
			LblCarregando.Text = "Carregando";
			await Task.Delay(250);
			LblCarregando.Text = "Carregando.";
			await Task.Delay(250);
			LblCarregando.Text = "Carregando..";
			await Task.Delay(250);
			LblCarregando.Text = "Carregando...";
			await Task.Delay(250);
		}
	}
}
