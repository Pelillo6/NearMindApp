using NearMindApp.Models;
using NearMindApp.Services;
using System.Collections.ObjectModel;
using System.Globalization;

namespace NearMindApp.Views;

public partial class CalendarioPage : ContentPage
{
    public ObservableCollection<Evento> Eventos { get; set; }

    private SupabaseService _supabaseService;
    private DateTime _currentMonth;
    public CalendarioPage()
	{
        InitializeComponent();
    }

    private void OnSelectionChanged(object sender, Syncfusion.Maui.Calendar.CalendarSelectionChangedEventArgs e)
    {
        var fechaSeleccionada = e.NewValue;
        if (fechaSeleccionada != null)
        {
            Console.WriteLine($"Fecha seleccionada: {fechaSeleccionada:dd/MM/yyyy}");
        }
    }


}