using Facilidata.FaciliHosp.Application.ViewModels;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Application.Interfaces
{
    public interface IExameService
    {
        bool Deletar(string id);
        EditarExameViewModel Editar(string id, string hospitalId, string usuarioId);
        bool Salvar(EditarExameViewModel viewModel);
        List<ExamePacientesViewModel> ObterPacientes(string hospitalId);
        ExameIndexViewModel ObterExamesPorHospitaIdEUsuarioId(string hospitalId, string usuarioId);
    }
}
