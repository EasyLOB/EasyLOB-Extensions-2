// DocsBr
// https://github.com/martinusso/docsbr.net
// https://www.nuget.org/packages/docsbr

// C # - Validando CNPJ , CPF e PIS
// http://www.macoratti.net/11/09/c_val1.htm

// Classe de Validação de CNPJ,CPF,CEP e Email
// http://codigofonte.uol.com.br/codigos/classe-de-validacao-de-cnpjcpfcep-e-email

using DocsBr;

namespace EasyLOB.Extensions.Brazil
{
    public static class BrazilHelper
    {
        #region Methods CNPJ

        public static bool IsCNPJ(string cnpj)
        {
            CNPJ cnpjBR = cnpj;

            return cnpjBR.IsValid();
        }

        public static string CNPJMascara(string cnpj, bool mascara = true)
        {
            CNPJ cnpjBR = cnpj;

            if (mascara)
            {
                return cnpjBR.ComMascara();
            }
            else
            {
                return cnpjBR.SemMascara();
            }
        }

        #endregion Methods CNPJ

        #region Methods CPF

        public static bool IsCPF(string cpf)
        {
            CPF cpfBR = cpf;

            return cpfBR.IsValid();
        }

        public static string CPFMascara(string cpf, bool mascara = true)
        {
            CPF cpfBR = cpf;

            if (mascara)
            {
                return cpfBR.ComMascara();
            }
            else
            {
                return cpfBR.SemMascara();
            }
        }

        #endregion Methods CPF

        #region Methods IE

        public static bool IsIE(string ie, int ufIBGE)
        {
            IE ieBR = new IE(ie, ufIBGE);

            return ieBR.IsValid();
        }

        public static bool IsIE(string ie, string uf)
        {
            IE ieBR = new IE(ie, uf);

            return ieBR.IsValid();
        }

        #endregion Methods IE

        #region Methods Telefone

        public static bool IsCodigoPais(string codigoPais)
        {
            return true;
        }

        public static bool IsCodigoArea(string codigoArea)
        {
            return true;
        }

        public static bool IsTelefone(string telefone)
        {
            return true;
        }

        #endregion Methods

        #region Methods

        public static bool IsCEP(string cep)
        {
            if (cep.Length == 8)
            {
                cep = cep.Substring(0, 5) + "-" + cep.Substring(5, 3);
            }

            return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
        }

        public static bool IsEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, ("(?<user>[^@]+)@(?<host>.+)"));
        }

        #endregion Methods

        #region ...

        /*
        public static bool IsCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool IsCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool IsPIS(string pis)
        {
            int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;

            if (pis.Trim().Length != 11)
                return false;
            pis = pis.Trim();
            pis = pis.Replace("-", "").Replace(".", "").PadLeft(11, '0');

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(pis[i].ToString()) * multiplicador[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            return pis.EndsWith(resto.ToString());
        }
         */

        #endregion ...
    }
}