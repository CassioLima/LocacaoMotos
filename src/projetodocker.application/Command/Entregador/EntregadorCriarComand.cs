using Enums;
using Flunt.Notifications;
using Flunt.Validations;
using MassTransit.Internals.GraphValidation;
using MediatR;
using Shared.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Command
{
    public class EntregadorCriarComand : Notifiable<Notification>, IRequest<CommandResult>
    {
        public EntregadorCriarComand(string nome, string cnpj, DateTime dataNascimento, string numeroCNH, string tipoCNH, string imagemCNH)
        {
            Nome = nome;
            CNPJ = cnpj;
            Data_Nascimento = dataNascimento;
            Numero_CNH = numeroCNH;
            Tipo_CNH = tipoCNH;
            Imagem_CNH = imagemCNH;

            AddNotifications(new Contract<EntregadorCriarComand>()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, "Nome", "Nome não informado!")
                .IsNotNullOrWhiteSpace(CNPJ, "CNPJ", "CNPJ não informado!")
                .IsTrue(ValidateCNPJ(CNPJ), "CNPJ", "CNPJ inválido!")
                .IsLowerOrEqualsThan(Data_Nascimento, DateTime.Today, "DataNascimento", "Data de nascimento inválida!")
                .IsNotNullOrWhiteSpace(Numero_CNH, "NumeroCNH", "Número da CNH não informado!")
                .IsNotNullOrWhiteSpace(Tipo_CNH, "TipoCNH", "Tipo da CNH não informado!")
                .IsTrue(ValidateCNHType(Tipo_CNH), "TipoCNH", "Tipo de CNH inválido! Somente é aceito (A, B ou AB)")
            );
        }
        public Guid Identificador { get; set; }
        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public DateTime Data_Nascimento { get; set; }
        public string Numero_CNH { get; set; }
        public string Tipo_CNH { get; set; }
        public string Imagem_CNH { get; set; }

        private bool ValidateCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove caracteres não numéricos
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            // Verifica o comprimento do CNPJ (deve ter 14 dígitos)
            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais (CNPJs com todos os dígitos iguais são inválidos)
            if (cnpj.Distinct().Count() == 1)
                return false;

            // Calcula os dígitos verificadores
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Calcula o primeiro dígito verificador
            string tempCNPJ = cnpj.Substring(0, 12);
            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCNPJ[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            int primeiroDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            // Calcula o segundo dígito verificador
            tempCNPJ = tempCNPJ + primeiroDigitoVerificador;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCNPJ[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            int segundoDigitoVerificador = resto < 2 ? 0 : 11 - resto;

            // Verifica se os dígitos calculados correspondem aos dígitos fornecidos
            return cnpj.EndsWith(primeiroDigitoVerificador.ToString() + segundoDigitoVerificador.ToString());

        }
        private bool ValidateCNHType(string tipoCnh)
        {
            if (tipoCnh == "A")
                return true;
            else if (tipoCnh == "B")
                return true;
            else if (tipoCnh == "AB")
                return true;

            return false;
        }
        public EntregadorCreated Map()
        {
            return new EntregadorCreated
            {
                Nome = Nome,
                CNPJ = CNPJ,
                DataNascimento = Data_Nascimento,
                NumeroCNH = Numero_CNH,
                TipoCNH = Tipo_CNH,
                ImagemCNH = Imagem_CNH,
            };
        }
    }
}

