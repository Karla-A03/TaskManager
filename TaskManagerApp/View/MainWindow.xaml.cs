using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManagerApp.Controller;
using Task = TaskManagerApp.Model.Task;

namespace TaskManagerApp
{
    public partial class MainWindow : Window
    {
        private TaskController _controller;

        public MainWindow()
        {
            InitializeComponent();
            _controller = new TaskController(this);

            // Evento para filtrar tareas por categoría
            cmbFiltroCategoria.SelectionChanged += CmbFiltroCategoria_SelectionChanged;

            // Event Handlers para marcar tareas y eliminar tareas
            btnMarcarCompletada.Click += BtnMarcarCompletada_Click;
            btnEliminarTarea.Click += BtnEliminarTarea_Click;
        }

        private void CmbFiltroCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var categoriaSeleccionada = (cmbFiltroCategoria.SelectedItem as ComboBoxItem)?.Content?.ToString();
            if (!string.IsNullOrEmpty(categoriaSeleccionada))
            {
                _controller.FiltrarPorCategoria(categoriaSeleccionada);
            }
        }

        private void BtnMarcarCompletada_Click(object sender, RoutedEventArgs e)
        {
            if (lvTareas.SelectedItem is Task tareaSeleccionada)
            {
                _controller.MarcarTareaComoCompletada(tareaSeleccionada);
            }
        }

        private void BtnEliminarTarea_Click(object sender, RoutedEventArgs e)
        {
            if (lvTareas.SelectedItem is Task tareaSeleccionada)
            {
                _controller.EliminarTarea(tareaSeleccionada);
            }
        }

    }
}