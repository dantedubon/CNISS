namespace CNISS.EnterpriseDomain.Domain.ValueObjects
{
    public class HorarioLaboral
    {
        public Hora HoraEntrada { get; protected set; }
        public Hora HoraSalida { get; protected set; }
        public DiasLaborables DiasLaborables { get; protected set; }

        protected HorarioLaboral()
        {
            
        }
        public HorarioLaboral(Hora horaEntrada, Hora horaSalida, DiasLaborables diasLaborables)
        {
            this.HoraSalida = horaSalida;
            this.HoraEntrada = horaEntrada;
            this.DiasLaborables = diasLaborables;
        }
    }
}