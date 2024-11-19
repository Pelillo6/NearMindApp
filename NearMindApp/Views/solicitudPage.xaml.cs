namespace NearMindApp.Views;

public partial class solicitudPage : ContentPage
{
    // Lista de datos para la tabla
    private List<List<string>> _datosSolicitudes;

    public solicitudPage()
    {
        InitializeComponent();

        // Inicializamos la tabla con datos predefinidos
        _datosSolicitudes = new List<List<string>>
        {
            new List<string> { "Juan P�rez", "2024-11-15", "S�" },
            new List<string> { "Ana G�mez", "2024-11-16", "No" },
            new List<string> { "Carlos Ruiz", "2024-11-17", "S�" }
        };

        // Enlazamos los datos al CollectionView
        SolicitudesTable.ItemsSource = _datosSolicitudes;
    }

    // M�todo que se ejecuta al pulsar el bot�n "Validar"
    private async void OnValidarClicked(object sender, EventArgs e)
    {
        // Obtenemos la fila seleccionada
        var selectedRow = SolicitudesTable.SelectedItem as List<string>;
        if (selectedRow != null)
        {
            // Mostramos el mensaje de validaci�n
            await DisplayAlert("Validaci�n", "Psic�logo validado correctamente", "OK");

            // Eliminamos la fila seleccionada
            _datosSolicitudes.Remove(selectedRow);

            // Actualizamos la tabla
            SolicitudesTable.ItemsSource = null;
            SolicitudesTable.ItemsSource = _datosSolicitudes;
        }
        else
        {
            // Si no hay selecci�n, mostramos un mensaje
            await DisplayAlert("Advertencia", "Por favor, seleccione una solicitud para validar.", "OK");
        }
    }
}
