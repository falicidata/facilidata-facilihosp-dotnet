﻿using Facilidata.FaciliHosp.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Facilidata.FaciliHosp.Application.ViewModels
{
    public class EditarExameViewModel
    {
        public string Id { get; set; }
        public string UsuarioId { get; set; }
        public string Medico { get; set; }
        public string TipoId { get; set; }

        [Required(ErrorMessage = "Descrição Obrigatório")]
        public string TipoOutro { get; set; }
        public string FornecedorId { get; set; }

        [Required(ErrorMessage = "Formato Obrigatório")]
        public EFormatoExame Formato { get; set; }
        public EExameResultadoAvaliacao ResultadoAvaliacao { get; set; } = EExameResultadoAvaliacao.Nenhum;

        [Required(ErrorMessage = "Fornecedor Obrigatório")]
        public string Fornecedor { get; set; }
        public string Resultado { get; set; }
        public string Retorno { get; set; }
        public string RetornoUsuario{ get; set; }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string NomeArquivo { get; set; }
        public string Base64Anexo { get; set; }
        public IFormFile Upload { get; set; }

    }
}
