namespace EventoWinClient;

public partial class FormPrincipal : Form
{
    public FormPrincipal()
    {
        InitializeComponent();
    }

    private void BtnSair_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnEventos_Click(object sender, EventArgs e)
    {
        var form = new FormEventos();
        form.ShowDialog();
    }

    private void BtnParticipante_Click(object sender, EventArgs e)
    {
        var form = new FormParticipante();
        form.ShowDialog();
    }

    private void BtnSincronizar_Click(object sender, EventArgs e)
    {

    }
}
