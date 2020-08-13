using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Enums;
using Facilidata.FaciliHosp.Domain.Models;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Application.Interfaces
{
    public interface IExameService
    {
        bool RemoverCompartilhado(string id);
        List<CompartilhadosViewModel> ObterCompartilhadosPorUsuarioCompartilhado();
        List<CompartilhadosViewModel> ObterCompartilhados();
        string GerarCodigoComp(string exameId, EPeriodoComp periodo = EPeriodoComp.Hora);
        void AdicionarRetorno(string exameId, EExameResultadoAvaliacao avaliacao, string retorno);
        bool CompartilharExame(string key);
        void RemoverAnexo(EditarExameViewModel viewModel);
        bool Deletar(string id);
        EditarExameViewModel Editar(string id);
        bool Salvar(EditarExameViewModel viewModel);
        List<ExameSemAnexo> ObterExamesUsuarioLogado();
        List<TipoExame> ObterExamesTipos();
    }
}
