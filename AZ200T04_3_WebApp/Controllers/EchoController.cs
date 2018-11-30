using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AZ200T04_3_WebApp.Controllers
{
    [Route("api/echo")]
    [ApiController]
    public class EchoController : ControllerBase
    {
        [HttpGet("{data}", Name = "GetEcho")]
        public string Echo(string data)
        {
            return $"{data.ToUpper()}...{data}...{data.ToLower()}";
        }
    }
}