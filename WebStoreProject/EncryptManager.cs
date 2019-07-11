using System.Text;

namespace WebStoreProject
{
    public class EncryptManager
    {

        public static string EncryptPass(string pass)
        {
            byte[] passes = Encoding.ASCII.GetBytes(pass);
            for (int i = 0; i < passes.Length; i++)
            {
                passes[i] ^= 97;
            }
            return Encoding.UTF8.GetString(passes);
        }
    }
}
