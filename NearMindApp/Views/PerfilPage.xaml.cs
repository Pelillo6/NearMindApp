using NearMindApp.Services;
using NearMindApp.Models;
using Supabase.Interfaces;
using Microsoft.Maui.Storage;
using Supabase.Storage;
using System.Diagnostics.Contracts;

namespace NearMindApp.Views;

public partial class PerfilPage : ContentPage
{
    private SupabaseService _supabaseService;
    private Usuario _usuarioActual;

    public PerfilPage()
    {
        InitializeComponent();
        _supabaseService = new SupabaseService();
        
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Obtener el usuario logueado
        _usuarioActual = UsuarioService.Instance.GetUsuarioActual();
        if (_usuarioActual != null)
        {
            if (_usuarioActual.isPsicologo()) {
                DescripcionLabel.Text = "Descripción";
                Descripcion.Placeholder = "¡Haz que tus pacientes te conozcan un poco más! Escribe a cerca de tu experiencia, métodos de trabajo, especialización...";
            } else if (!_usuarioActual.isPsicologo()) {
                DescripcionLabel.Text = "Información opcional";
                Descripcion.Placeholder = "¡Haz que tu psicólogo/a te conozca un poco más! La información que escribas será compartida únicamente con nuestros profesionales certificados.";
                EspecialidadLabel.IsVisible = false;
                Especialidad.IsVisible = false;
                PrecioLabel.IsVisible = false;
                Precio.IsVisible = false;
            } else {
                Descripcion.Placeholder = "Vista Administrador";
                EspecialidadLabel.IsVisible = false;
                Especialidad.IsVisible = false;
                PrecioLabel.IsVisible = false;
                Precio.IsVisible = false;
            }

            Nombre.Text = _usuarioActual.nombre;
            Telefono.Text = _usuarioActual.telefono;
            Ubicacion.Text = _usuarioActual.ubicacion;
            Correo.Text = _usuarioActual.email;
            Contra.Text = _usuarioActual.password;
            Descripcion.Text = _usuarioActual.descripcion;
            FecNacimiento.Date = _usuarioActual.fecha_nacimiento;

            if (_usuarioActual.isPsicologo())
            {
                Especialidad.Text = _usuarioActual.especialidad;
                Precio.Text = _usuarioActual.precio.ToString();
            }


            if (!string.IsNullOrEmpty(_usuarioActual.imagen_perfil))
            {
                var storage = _supabaseService.GetClient().Storage;
                var bucket = storage.From("imagenes-perfil");

                ImagenPerfil.Source = bucket.GetPublicUrl(_usuarioActual.imagen_perfil);
            }
        }
    }

    private async void OnChangeProfileImageClicked(object sender, EventArgs e) {

        // Verificar y solicitar permisos para acceder al almacenamiento
        var status = await Permissions.RequestAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permiso denegado", "No se puede acceder a las imágenes sin permisos.", "OK");
            return;
        }

        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Selecciona una imagen de perfil",
                FileTypes = FilePickerFileType.Images
            });

            if (result == null)
            {
                return;
            }

            var filePath = result.FullPath;
            var storage = _supabaseService.GetClient().Storage;
            var bucket = storage.From("imagenes-perfil");

            Random random = new Random();
            int randomNumber = random.Next(1, 100000);

            string fileName = $"{_usuarioActual.id}_" + randomNumber + ".png";

            var listaImagenes = await bucket.List("");

            while (listaImagenes.Any(item => item.Name == fileName))
            {
                fileName = $"{_usuarioActual.id}_" + randomNumber + ".png";
            }

            await bucket.Upload(filePath, fileName);

            ImagenPerfil.Source = bucket.GetPublicUrl(fileName);

            _usuarioActual.imagen_perfil = fileName;

            await _supabaseService.ActualizarElementoEnTabla(_usuarioActual.id, _usuarioActual);
            await DisplayAlert("Éxito", "Imagen de perfil actualizada correctamente.", "OK");
        }
        catch (Exception ex)
        {
            // Manejar errores
            await DisplayAlert("Error", $"No se pudo actualizar la imagen: {ex.Message}", "OK");
        }
    }

    private async void OnGuardarCambiosClicked(object sender, EventArgs e)
    {
        

        if (string.IsNullOrEmpty(Nombre.Text) || string.IsNullOrEmpty(Telefono.Text) ||
            string.IsNullOrEmpty(Ubicacion.Text) || string.IsNullOrEmpty(Correo.Text) ||
            string.IsNullOrEmpty(Contra.Text) || (_usuarioActual.isPsicologo() && 
            (string.IsNullOrEmpty(Precio.Text) || string.IsNullOrEmpty(Especialidad.Text))))
        {
            MensajeLabel.Text = "Por favor, complete todos los campos.";
            MensajeLabel.TextColor = Colors.Red;
            MensajeLabel.IsVisible = true;
            return;
        }

        double parsedPrecio;

        try
        {
            // Actualizar los datos en la base de datos
            _usuarioActual.nombre = Nombre.Text;
            _usuarioActual.telefono = Telefono.Text;
            _usuarioActual.ubicacion = Ubicacion.Text;
            _usuarioActual.email = Correo.Text;
            _usuarioActual.password = Contra.Text;
            _usuarioActual.descripcion = Descripcion.Text;
            _usuarioActual.fecha_nacimiento = FecNacimiento.Date;
            if (_usuarioActual.isPsicologo()) {
                if (double.TryParse(Precio.Text, out parsedPrecio))
                {
                    _usuarioActual.precio = parsedPrecio;
                }
                else
                {
                    MensajeLabel.Text = "El campo Precio debe contener un valor numérico válido.";
                    MensajeLabel.TextColor = Colors.Red;
                    MensajeLabel.IsVisible = true;
                    return;
                }

                _usuarioActual.especialidad = Especialidad.Text;
            }

            await _supabaseService.ActualizarElementoEnTabla(_usuarioActual.id, _usuarioActual);

            // Actualizar el servicio compartido
            UsuarioService.Instance.SetUsuarioActual(_usuarioActual);

            Nombre.IsEnabled = false;
            Telefono.IsEnabled = false;
            Ubicacion.IsEnabled = false;
            Correo.IsEnabled = false;
            Contra.IsEnabled = false;
            Especialidad.IsEnabled = false;
            Precio.IsEnabled = false;
            FecNacimiento.IsEnabled = false;
            BtnGuardar.IsVisible = false;
            BtnEditar.IsVisible = true;

            // Mostrar mensaje de éxito
            MensajeLabel.Text = "Cambios guardados correctamente.";
            MensajeLabel.TextColor = Colors.Green;
            MensajeLabel.IsVisible = true;
        }
        catch (Exception ex)
        {
            MensajeLabel.Text = $"Error al guardar cambios: {ex.Message}";
            MensajeLabel.TextColor = Colors.Red;
            MensajeLabel.IsVisible = true;
        }
    }

    private async void OnEditarClicked(object sender, EventArgs e) {
        Nombre.IsEnabled = true;
        Telefono.IsEnabled = true;
        Ubicacion.IsEnabled = true;
        Correo.IsEnabled = true;
        Contra.IsEnabled = true;
        Especialidad.IsEnabled = true;
        Precio.IsEnabled = true;
        FecNacimiento.IsEnabled = true;
        BtnGuardar.IsVisible = true;
        BtnEditar.IsVisible = false;
    }
}