﻿using DataLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SuperHeroApi.Controllers
{
    public class HeroControler : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            Object objectValue = GenericDatabase.ExecuteCommand("Select * FROM Herois", System.Data.CommandType.Text, null, GenericDatabase.ExecutionType.ExecuteDataTable);

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
