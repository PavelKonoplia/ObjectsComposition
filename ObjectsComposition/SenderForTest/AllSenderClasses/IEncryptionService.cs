namespace PortSender
{
    public interface IEncryptionService
    {
        object Decrypt(byte[] cipherObj);

        byte[] Encrypt(object decipherObj);
    }
}
