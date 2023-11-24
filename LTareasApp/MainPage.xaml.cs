using System.Collections.ObjectModel;
using LTareasApp.Models;


namespace LTareasApp
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Tarea> Tareas { get; set; }

        public MainPage()
        {
            InitializeComponent();
            Tareas = new ObservableCollection<Tarea>();
            tareasListView.BindingContext = this;
            UpdateNoTareasLabelVisibility();
        }

        private void OnAgregarTareaClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tareaEntry.Text))
            {
                Tareas.Add(new Tarea { Nombre = tareaEntry.Text });
                tareaEntry.Text = string.Empty;
                UpdateNoTareasLabelVisibility();
            }
        }

        private void OnTareaCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            var checkBox = (sender as CheckBox);
            var tarea = checkBox?.BindingContext as Tarea;
            if (tarea != null)
            {
                tarea.Completada = e.Value;
            }
        }

        private void OnEliminarTareaClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var tarea = button.BindingContext as Tarea;
                if (tarea != null)
                {
                    int index = Tareas.IndexOf(tarea);

                    if (index != -1)
                    {
                        Tareas.RemoveAt(index);

                        UpdateNoTareasLabelVisibility();

                        tareasListView.ItemsSource = null;
                        tareasListView.ItemsSource = Tareas;

                        MostrarAlerta("Tarea Borrada", "La tarea ha sido borrada exitosamente.");
                    }
                }
            }
        }

        private async void MostrarAlerta(string titulo, string mensaje)
        {
            await DisplayAlert(titulo, mensaje, "Aceptar");
        }


        private void UpdateNoTareasLabelVisibility()
        {
            noTareasLabel.IsVisible = Tareas.Count == 0;

        }
    }
}
