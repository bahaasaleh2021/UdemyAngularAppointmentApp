using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class CreateMessageDTO
    {
        public string RecepientUserName { get; set; }
        public string Content { get; set; }
    }
}