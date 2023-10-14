using System.Text.RegularExpressions;

namespace LocalidadesAPI.Helpers
{
    public static class ValidacaoHelper
    {
        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) 
                return false;

            string emailRegex = string.Format("{0}{1}",
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))",
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");

            return Regex.IsMatch(email, emailRegex);
        }

        public static bool ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha) || senha.Length < 6)
                return false;

            return true;
        }
    }
}
