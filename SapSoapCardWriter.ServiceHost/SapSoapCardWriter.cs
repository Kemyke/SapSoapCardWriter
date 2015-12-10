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

namespace SapSoapCardWriter.ServiceHost
{
    [ServiceBehavior(Namespace = "http://sapsoapcardwriter.com", InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, AddressFilterMode = AddressFilterMode.Any)]
    public class SapSoapCardWriter : ISapSoapCardWriter
    {
        private readonly ILogger logger;
        private readonly ICardWriter cardWriter;
        private readonly ISapSoapCardWriterConfig config;

        public SapSoapCardWriter(ILogger logger, ISapSoapCardWriterConfig config, ICardWriter cardWriter)
        {
            this.logger = logger;
            this.config = config;
            this.cardWriter = cardWriter;
        }

        public Response WriteCard(DTO.User user)
        {
            logger.Debug("WriteCard called!");

            User blUser = new User() { Name = user.Name, Address = user.Address };
            ResultCode rc = cardWriter.WriteCard(blUser);
            return new Response(rc);
        }
    }
}
