using SapSoapCardWriter.BusinessLogic;
using SapSoapCardWriter.Logger.Logging;
using SapSoapCardWriter.ServiceContracts;
using DTO = SapSoapCardWriter.ServiceContracts.DTO;
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

        public Response WriteCard(DTO.User user)
        {
            try
            {
                logger.Debug("WriteCard called!");

                User blUser = new User() { Name = user.Name, Address = user.Address };
                ResultCode rc = cardWriter.WriteCard(blUser);
                return new Response(rc);
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                return new Response(ResultCode.UNKNOWN_ERROR);
            }
        }


        public Response<string> Encrypt(string payload, string key)
        {
            try
            {
                logger.Debug("Encrypt called!");
                string chiper = encryptor.Encrypt(payload, key);
                return new Response<string>(ResultCode.OK, chiper);
            }
            catch(Exception ex)
            {
                logger.Error(ex.ToString());
                return new Response<string>(ResultCode.UNKNOWN_ERROR, null);
            }
        }

        public Response<string> Decrypt(string chiper, string key)
        {
            try
            {
                logger.Debug("Decrypt called!");
                string clearText = encryptor.Encrypt(chiper, key);
                return new Response<string>(ResultCode.OK, clearText);
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                return new Response<string>(ResultCode.UNKNOWN_ERROR, null);
            }
        }
    }
}
