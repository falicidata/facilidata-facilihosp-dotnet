﻿using Facilidata.FaciliHosp.Application.ViewModels;
using Facilidata.FaciliHosp.Domain.Models;
using System.Collections.Generic;

namespace Facilidata.FaciliHosp.Application.Interfaces
{
    public interface IExameService
    {
        void RemoverAnexo(EditarExameViewModel viewModel);
        bool Deletar(string id);
        EditarExameViewModel Editar(string id);
        bool Salvar(EditarExameViewModel viewModel);
       List<ExameSemAnexo> ObterExamesUsuarioLogado();
    }
}
