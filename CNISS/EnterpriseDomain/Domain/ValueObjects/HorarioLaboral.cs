namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class HorarioLaboral
    {
        public Hora horaEntrada { get; protected set; }
        public Hora horaSalida { get; protected set; }
        public DiasLaborables diasLaborables { get; protected set; }

        protected HorarioLaboral()
        {
            
        }
        public HorarioLaboral(Hora horaSalida, Hora horaEntrada, DiasLaborables diasLaborables)
        {
            this.horaSalida = horaSalida;
            this.horaEntrada = horaEntrada;
            this.diasLaborables = diasLaborables;
        }
    }
}