using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TaskManagerApp.Model;
using Task = TaskManagerApp.Model.Task;

namespace TaskManagerApp.Controller
{
    public class TaskController
    {
        private MainWindow _view;
        private TaskManager _taskManager;

        public TaskController(MainWindow view)
        {
            _view = view;
            _taskManager = new TaskManager();

            // Asignar eventos
            _view.btnAgregarTarea.Click += BtnAgregarTarea_Click;

            // Inicializar lista
            ActualizarListaTareas();
        }

        private void BtnAgregarTarea_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Leer datos de los campos
            string titulo = _view.txtTitulo.Text;
            string descripcion = _view.txtDescripcion.Text;
            string categoria = _view.txtCategoria.Text;
            string prioridad = (_view.cmbPrioridad.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (string.IsNullOrWhiteSpace(titulo) || prioridad == null)
                return;

            // Crear y agregar tarea
            string prioridadStr = (_view.cmbPrioridad.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (!Enum.TryParse(prioridadStr, out Priority prioridadEnum))
            {
                return;
            }

            var tarea = new Task(titulo, descripcion, categoria, prioridadEnum);
            _taskManager.AgregarTarea(tarea);

            // Actualizar lista
            ActualizarListaTareas();

            // Limpiar campos
            _view.txtTitulo.Text = "";
            _view.txtDescripcion.Text = "";
            _view.txtCategoria.Text = "";
            _view.cmbPrioridad.SelectedIndex = -1;
        }

        private void ActualizarListaTareas()
        {
            _view.lvTareas.ItemsSource = null;
            _view.lvTareas.ItemsSource = _taskManager.ObtenerTodasLasTareas();

            // Actualizar tareas en cola
            _view.txtTareasEnCola.Text = $"Tarea pendiente: {_taskManager.ObtenerSiguienteTarea()?.Titulo ?? "No hay tareas pendientes"}";
        }

        // Método para filtrar tareas por categoría
        public void FiltrarPorCategoria(string categoria)
        {
            var tareasFiltradas = _taskManager.ObtenerTareasPorCategoria(categoria);
            _view.lvTareas.ItemsSource = tareasFiltradas;
        }

        // Método para marcar tarea como completada
        public void MarcarTareaComoCompletada(Task tarea)
        {
            _taskManager.MarcarTareaComoCompletada(tarea);
            ActualizarListaTareas();
        }

        // Método para eliminar una tarea
        public void EliminarTarea(Task tarea)
        {
            _taskManager.EliminarTarea(tarea);
            ActualizarListaTareas();
        }

        // Método para obtener y mostrar tareas en cola
        public void MostrarTareasEnCola()
        {
            var tareasEnCola = _taskManager.ObtenerSiguienteTarea();
            if (tareasEnCola != null)
            {
                // Mostrar en una etiqueta o en cualquier otro lugar
                _view.txtTareasEnCola.Text = $"Siguiente tarea: {tareasEnCola.Titulo}";
            }
        }




    }
}
