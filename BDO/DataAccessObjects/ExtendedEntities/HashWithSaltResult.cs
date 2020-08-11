using System;
using System.Collections.Generic;
using System.Text;

namespace BDO.DataAccessObjects.ExtendedEntities
{
    public class HashWithSaltResult
    {
        public string Salt { get; }
        public string Digest { get; set; }
        public HashWithSaltResult(string digest, string salt)
        {
            Digest = digest;
            Salt = salt;
        }
    }
}
