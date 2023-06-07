using System.Security.Cryptography;

namespace JamesJonesApplication.Services
{
    public class EncryptionService
    {
        private readonly string _secretKey;

        //Access to the Unique Key
        public EncryptionService(IConfiguration config)
        {
            _secretKey = config["SecretKey"] ?? "";
        }

        public byte[] EncryptByteArray(byte[] encryptData) 
        {

            using (AesManaged aesAlgorithm = new AesManaged())
            {
                //Feeding 128 bit key into the system by converting the key into byte array[]
                aesAlgorithm.Key = System.Text.Encoding.UTF8.GetBytes(_secretKey);

                //Create crypto transform                                     //Generates its own generation
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

                //Area to work by putting a chunk of process
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    //Configuration for the encryption
                    msEncrypt.Write(aesAlgorithm.IV, 0, 16);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        //byte array specifiied
                        csEncrypt.Write(encryptData, 0, encryptData.Length);

                        
                        csEncrypt.FlushFinalBlock();

                        //Gives back encrypted data or result
                        return msEncrypt.ToArray();
                    }

                }
            }
        }

        public byte[] DecryptByteArray(byte[] decyptData) 
        {
            using(AesManaged aesAlgorithm = new AesManaged())
            {
                aesAlgorithm.Key = System.Text.Encoding.UTF8.GetBytes(_secretKey);

                byte[] IV = new byte[16];

                Array.Copy(decyptData, 0 , IV, 0, 16);

                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, IV);

                using (MemoryStream msDecrypt = new MemoryStream(decyptData)) 
                {
                    //Should be write after decrypting as to rewrite the decrypted data
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(decyptData, 16, decyptData.Length - 16);

                        csDecrypt.FlushFinalBlock();

                        return msDecrypt.ToArray();
                    }
                }
            }
        }



    }
}
