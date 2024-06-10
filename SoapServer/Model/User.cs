﻿using System.Runtime.Serialization;

namespace SoapServer.Model
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }
    }
}
