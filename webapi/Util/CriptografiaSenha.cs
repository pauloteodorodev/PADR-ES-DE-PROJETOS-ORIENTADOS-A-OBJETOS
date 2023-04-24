using System.Security.Cryptography;

namespace webapi.Util
{
    public static class CriptografiaSenha
    {
        private const int TamanhoSalt = 16;
        private const int TamanhoHash = 32;

        public static string CriarSalt()
        {
            // Gera um salt aleatório de 16 bytes
            byte[] salt = new byte[TamanhoSalt];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string CriptografarSenha(string senha, string salt)
        {
            byte[] senhaBytes = System.Text.Encoding.UTF8.GetBytes(senha);
            byte[] saltBytes = Convert.FromBase64String(salt);

            byte[] saltedSenhaBytes = new byte[saltBytes.Length + senhaBytes.Length];
            Array.Copy(saltBytes, saltedSenhaBytes, saltBytes.Length);
            Array.Copy(senhaBytes, 0, saltedSenhaBytes, saltBytes.Length, senhaBytes.Length);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashedSenhaBytes = sha256.ComputeHash(saltedSenhaBytes);
                return Convert.ToBase64String(hashedSenhaBytes);
            }
        }

        public static bool VerificarSenha(string senha, string salt, string hashedSenha)
        {
            string hashedSenhaInput = CriptografarSenha(senha, salt);
            return hashedSenhaInput == hashedSenha;
        }
    }
}
