using System;
using System.Data;
using Data;

namespace Logic
{
    public class QuotesLog
    {
        private QuotesDat objQuotes = new QuotesDat();

        // Método para mostrar todas las citas
        public DataSet showQuotes()
        {
            return objQuotes.showQuotes();
        }

        // Método para mostrar solo el ID y la descripción de las citas
        public DataSet showQuotesDDL()
        {
            return objQuotes.showQuotesDDL();
        }

        // Método para insertar una nueva cita
        public bool saveQuote(DateTime fecha, TimeSpan hora, string estado, int fkPaciId, int fkOdoId)
        {
            return objQuotes.saveQuote(fecha, hora, estado, fkPaciId, fkOdoId);
        }

        // Método para actualizar una cita
        public bool updateQuote(int citaId, DateTime fecha, TimeSpan hora, string estado, int fkPaciId, int fkOdoId)
        {
            return objQuotes.updateQuote(citaId, fecha, hora, estado, fkPaciId, fkOdoId);
        }

        // Método para eliminar una cita
        public bool deleteQuote(int citaId)
        {
            return objQuotes.deleteQuote(citaId);
        }
    }
}
