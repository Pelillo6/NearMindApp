using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NearMindApp.Services;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Supabase;
using NearMindApp.Models;

namespace NearMindApp.ModelViews
{
    public partial class CalendarioViewModel : ObservableObject
    {
        public ObservableCollection<DiaCalendario> DiasDelMes { get; set; } = new ObservableCollection<DiaCalendario>();

        [ObservableProperty]
        private DiaCalendario diaSeleccionado;

        public CalendarioViewModel()
        {
            _supabaseClient = new Client("https://ypjbezsniccydiqdhnvs.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InlwamJlenNuaWNjeWRpcWRobnZzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mjk4NzUwMjEsImV4cCI6MjA0NTQ1MTAyMX0.VJiINPZnNn9NCaCQrHwwUe51MCitZl-gT4AjI5nPhJw");
            CargarCitasAsync();
        }

        private Client _supabaseClient;
        private void GenerarDiasDelMes(Dictionary<DateTime, string> citas)
        {
            DiasDelMes.Clear();

            var totalDias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            for (int i = 1; i <= totalDias; i++)
            {
                var fecha = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                DiasDelMes.Add(new DiaCalendario
                {
                    Dia = i,
                    Fecha = fecha,
                    TieneCita = citas.ContainsKey(fecha),
                    DetallesCita = citas.ContainsKey(fecha) ? citas[fecha] : string.Empty
                });
            }
        }

        private async Task CargarCitasAsync()
        {
            var usuarioId = UsuarioService.Instance.GetUsuarioActual().id;

            var resultado = await _supabaseClient.From<Cita>()
                .Where(c => c.usuario1_id == usuarioId || c.usuario2_id == usuarioId)
                .Get();

            var citas = resultado.Models.ToDictionary(
                c => c.fecha.Date,
                c => c.nota
            );

            GenerarDiasDelMes(citas);
        }
    }

    public class DiaCalendario
    {
        public int Dia { get; set; }
        public DateTime Fecha { get; set; }
        public bool TieneCita { get; set; }
        public string DetallesCita { get; set; }
    }
}