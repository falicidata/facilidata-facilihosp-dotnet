using Facilidata.FaciliHosp.Infra.Identity.Entidades;
using System;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Infra.Identity.ViewModels
{
    public class PlanoViewModel
    {
        public string PlanoAtual { get; set; }
        public List<Plano> Planos { get; set; }
    }
}
