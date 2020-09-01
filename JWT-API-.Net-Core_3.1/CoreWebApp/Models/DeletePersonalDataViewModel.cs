using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp.Models
{
    public class DeletePersonalDataViewModel : BDO.DataAccessObjects.SecurityModule.owin_userEntity
    {
        public bool RequirePassword { get; set; }
    }
}
