using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WpfOpenCVTest.Dal.Models;

namespace WpfOpenCVTest.BLL.DataCVHelper
{
public class JsonHelper
    {
        private readonly ILogger<JsonHelper> _logger;

        public JsonHelper()
        { }
        public JsonHelper(ILogger<JsonHelper> logger)
        {
            _logger = logger;
        }

        public dynamic Jconvertor( string JsonStr )
        {
            JObject obj = new JObject();
            try
            {   
                var jstr = JsonStr;
                if (!string.IsNullOrEmpty(jstr) && !string.IsNullOrWhiteSpace(jstr))
                     obj = JsonConvert.DeserializeObject<JObject>(jstr);
                return obj;
            }           
            catch (Exception ex)
            {
               _logger.LogError(ex.Message);
                throw ex;
            }
        }

        public bool CheckDataFormat(string JsonStr)
        {
            bool flag = false;
            try
            {
                var jstr = JsonStr;
                if (!string.IsNullOrEmpty(jstr) && !string.IsNullOrWhiteSpace(jstr))
                    flag = true;    
                return flag;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw ex;
            }
        }


    }
}
