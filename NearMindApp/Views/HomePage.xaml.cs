namespace NearMindApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnLoginPacienteButtonClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new RegistroPacientePage());
    }

    private async void OnLoginProfesionalButtonClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new RegistroProfesionalPage());
    }
}