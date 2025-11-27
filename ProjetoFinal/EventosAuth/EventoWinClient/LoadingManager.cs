namespace EventoWinClient;

public static class LoadingManager
{
	private static FormLoading? _loadingForm;

	public static void ShowLoading(Form parent)
	{
		if (_loadingForm != null)
			return;

		_loadingForm = new()
		{
			StartPosition = FormStartPosition.CenterScreen
		};

		parent.Enabled = false;

		// Exibe no mesmo thread da UI (correto)
		_loadingForm.Show(parent);
		_loadingForm.BringToFront();
		_loadingForm.Update();
	}

	public static void HideLoading(Form parent)
	{
		if (_loadingForm == null)
			return;

		parent.Enabled = true;

		_loadingForm.Close();
		_loadingForm.Dispose();
		_loadingForm = null;
	}
}
