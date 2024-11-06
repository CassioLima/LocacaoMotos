using Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Command
{
    public class EntregadorAtualizarComand : Notifiable<Notification>, IRequest<CommandResult>
    {

        public EntregadorAtualizarComand(int id, string nome, string cnpj, DateTime dataNascimento, string numeroCNH, string tipoCNH, string imagemCNH)
        {
            Id = id;
            Nome = nome;
            CNPJ = cnpj;
            DataNascimento = dataNascimento;
            NumeroCNH = numeroCNH;
            TipoCNH = tipoCNH;
            ImagemCNH = imagemCNH;

            AddNotifications(new Contract<EntregadorAtualizarComand>()
                .Requires()
                .IsGreaterThan(Id, 0, "Id", "Id do entregador não informado!")
                .IsNotNullOrWhiteSpace(Nome, "Nome", "Nome não informado!")
                .IsNotNullOrWhiteSpace(CNPJ, "CNPJ", "CNPJ não informado!")
                .IsTrue(ValidarCNPJ(CNPJ), "CNPJ", "CNPJ inválido!")
                .IsLowerOrEqualsThan(DataNascimento, DateTime.Today, "DataNascimento", "Data de nascimento inválida!")
                .IsNotNullOrWhiteSpace(NumeroCNH, "NumeroCNH", "Número da CNH não informado!")
                .IsNotNullOrWhiteSpace(TipoCNH, "TipoCNH", "Tipo da CNH não informado!")
            );
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime DataNascimento { get; set; }
        public string NumeroCNH { get; set; }
        public string TipoCNH { get; set; }
        public string ImagemCNH { get; set; }

        private bool ValidarCNPJ(string cnpj)
        {
            // Implemente a validação de CNPJ aqui
            return true; // Supondo que o CNPJ é válido por enquanto
        }


    }
}
