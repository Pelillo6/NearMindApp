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
            new List<string> { "Juan Pérez", "2024-11-15", "Sí" },
            new List<string> { "Ana Gómez", "2024-11-16", "No" },
            new List<string> { "Carlos Ruiz", "2024-11-17", "Sí" }
        };

        // Enlazamos los datos al CollectionView
        SolicitudesTable.ItemsSource = _datosSolicitudes;
    }

    // Método que se ejecuta al pulsar el botón "Validar"
    private async void OnValidarClicked(object sender, EventArgs e)
    {
        // Obtenemos la fila seleccionada
        var selectedRow = SolicitudesTable.SelectedItem as List<string>;
        if (selectedRow != null)
        {
            // Mostramos el mensaje de validación
            await DisplayAlert("Validación", "Psicólogo validado correctamente", "OK");

            // Eliminamos la fila seleccionada
            _datosSolicitudes.Remove(selectedRow);

            // Actualizamos la tabla
            SolicitudesTable.ItemsSource = null;
            SolicitudesTable.ItemsSource = _datosSolicitudes;
        }
        else
        {
            // Si no hay selección, mostramos un mensaje
            await DisplayAlert("Advertencia", "Por favor, seleccione una solicitud para validar.", "OK");
        }
    }
}
