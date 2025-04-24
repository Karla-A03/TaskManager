using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp.Model
{
    public enum Priority { Alta, Media, Baja }
    public enum Status { Pendiente, Completada }

    public class Task
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Categoria { get; set; }
        public Priority Prioridad { get; set; }
        public Status Estado { get; set; }
        public DateTime FechaCreacion { get; set; }

        public Task(string titulo, string descripcion, string categoria, Priority prioridad)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            Categoria = categoria;
            Prioridad = prioridad;
            Estado = Status.Pendiente;
            FechaCreacion = DateTime.Now;
        }

        public void Completar()
        {
            Estado = Status.Completada;
        }
    }

}
