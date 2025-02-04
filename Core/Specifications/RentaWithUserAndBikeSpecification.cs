using System;
using Core.Entities;

namespace Core.Specifications;

public class RentaWithUserAndBikeSpecification : BaseSpecification<Renta>
{

    public RentaWithUserAndBikeSpecification(RentaSpecificationParams rentaParams) : base(
        x =>
        (string.IsNullOrEmpty(rentaParams.Search) || x.Usuario.Nombre.Contains(rentaParams.Search)) &&
        (rentaParams.UserID.HasValue || x.UsuarioID == rentaParams.UserID)&&
        (rentaParams.BicicletaID.HasValue || x.BicicletaID == rentaParams.BicicletaID)
    )
    {
        AddInclude(r=>r.Usuario);
        ApplyPaging(rentaParams.PageSize * (rentaParams.PageIndex -1 ), rentaParams.PageSize);

    }


    public RentaWithUserAndBikeSpecification(int id) :base(x=>x.Id == id){
        AddInclude(r=>r.Usuario);
    }
}
