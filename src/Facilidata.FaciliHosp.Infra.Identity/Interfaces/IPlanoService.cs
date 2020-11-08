using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Infra.Identity.Interfaces
{
    public interface IPlanoService
    {
        List<Plano> ObterPlanos();
    }
}
