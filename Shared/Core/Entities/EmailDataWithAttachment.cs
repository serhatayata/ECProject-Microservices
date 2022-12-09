using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class EmailDataWithAttachment:EmailData
    {
        public IFormFileCollection EmailAttachments { get; set; }

    }
}
