namespace apisiase.Dto
{
    public class ProfesorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<MateriaDto> Materias { get; set; }
    }



}
