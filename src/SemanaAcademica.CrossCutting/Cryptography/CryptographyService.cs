using SemanaAcademica.Domain.Contracts.CrossCutting.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace SemanaAcademica.CrossCutting.Cryptography
{
    public class CryptographyService : ICryptoghaphy
    {
        /// <summary>
        /// Encriptografia da senha do usuário
        /// </summary>
        /// <param name="value">Senha do usuário</param>
        /// <returns></returns>
        public string Encrypt(string value)
        {
            using var sha256 = SHA256.Create();
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(value));

            var stringBuilder = new StringBuilder(hashBytes.Length * 2);
            foreach (byte b in hashBytes)
            {
                stringBuilder.Append(b.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
