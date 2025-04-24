using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{
    public class TaskManager
    {
        // Map: Categorías → Lista de tareas
        private Dictionary<string, List<Task>> tareasPorCategoria;

        // Lista general de tareas
        private List<Task> listaGeneral;

        // Cola: tareas en orden de llegada
        private Queue<Task> colaTareas;

        public TaskManager()
        {
            tareasPorCategoria = new Dictionary<string, List<Task>>();
            listaGeneral = new List<Task>();
            colaTareas = new Queue<Task>();
        }

        public void AgregarTarea(Task tarea)
        {
            // Agrega a la lista general
            listaGeneral.Add(tarea);

            // Agrega a la cola
            colaTareas.Enqueue(tarea);

            // Agrega al diccionario según la categoría
            if (!tareasPorCategoria.ContainsKey(tarea.Categoria))
            {
                tareasPorCategoria[tarea.Categoria] = new List<Task>();
            }

            tareasPorCategoria[tarea.Categoria].Add(tarea);
        }

        public List<Task> ObtenerTodasLasTareas()
        {
            return listaGeneral;
        }

        public List<Task> ObtenerTareasPorCategoria(string categoria)
        {
            if (tareasPorCategoria.ContainsKey(categoria))
            {
                return tareasPorCategoria[categoria];
            }
            return new List<Task>();
        }

        public Task ObtenerSiguienteTarea()
        {
            return colaTareas.Count > 0 ? colaTareas.Peek() : null;
        }

        public void MarcarTareaComoCompletada(Task tarea)
        {
            tarea.Completar();
        }

        public void EliminarTarea(Task tarea)
        {
            listaGeneral.Remove(tarea);
            if (tareasPorCategoria.ContainsKey(tarea.Categoria))
            {
                tareasPorCategoria[tarea.Categoria].Remove(tarea);
            }
            Queue<Task> nuevaCola = new Queue<Task>();
            while (colaTareas.Count > 0)
            {
                Task t = colaTareas.Dequeue();
                if (t != tarea)
                    nuevaCola.Enqueue(t);
            }
            colaTareas = nuevaCola;
        }

        public List<Task> ObtenerTareasPorEstado(Status estado)
        {
            return listaGeneral.Where(t => t.Estado == estado).ToList();
        }
    }

}
