using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.Logger.Logging;
using SapSoapCardWriter.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SapSoapCardWriter.Common;
using System.ServiceModel;
using SapSoapCardWriter.Common.Encryption;

namespace SapSoapCardWriter.ServiceHost
{
    [ServiceBehavior(Namespace = "http://sapsoapcardwriter.com", InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, AddressFilterMode = AddressFilterMode.Any)]
    public class SapSoapCardWriter : ISapSoapCardWriter
    {
        private readonly ILogger logger;
        private readonly ICardWriter cardWriter;
        private readonly ISapSoapCardWriterConfig config;
        private readonly IEncryptor encryptor;

        public SapSoapCardWriter(ILogger logger, ISapSoapCardWriterConfig config, ICardWriter cardWriter, IEncryptor encryptor)
        {
            this.logger = logger;
            this.config = config;
            this.cardWriter = cardWriter;
            this.encryptor = encryptor;
        }

        public Response WriteCard(string key, List<string> dataList)
        {
            try
            {
                logger.Debug("WriteCard called!");

                ResultCode rc = cardWriter.WriteCard(key, dataList);
                return new Response(rc);
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                var resp = new Response(ResultCode.UNKNOWN_ERROR, ex.ToString());
                return resp;
            }
        }


        public Response<string> Encrypt(string payload, string key)
        {
            try
            {
                logger.Debug("Encrypt called!");
                string encryptedPayload = encryptor.Encrypt(payload, key);
                return new Response<string>(ResultCode.OK, encryptedPayload);
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                return new Response<string>(ResultCode.UNKNOWN_ERROR, null, ex.ToString());
            }
        }

        public Response<string> Decrypt(string encryptedPayload, string key)
        {
            try
            {
                logger.Debug("Decrypt called!");
                string clearText = encryptor.Decrypt(encryptedPayload, key);
                return new Response<string>(ResultCode.OK, clearText);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new Response<string>(ResultCode.UNKNOWN_ERROR, null, ex.ToString());
            }
        }
    }
}
