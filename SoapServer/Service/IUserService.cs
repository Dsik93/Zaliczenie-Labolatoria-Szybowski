using SoapServer.Model;
using System.ServiceModel;

namespace SoapServer.Service
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string RegisterUser(User user);
    }
}
