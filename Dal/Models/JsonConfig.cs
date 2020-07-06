using System;
using System.Collections.Generic;
using System.Text;

namespace WpfOpenCVTest.Dal.Models
{
public class JsonConfig
    {   
        public int Id { get; set; }
        /// <summary>
        /// 應用程式名稱
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 版本號
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 啟用時間
        /// </summary>
        public string Cdate { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        public string Udate { get; set; }
        /// <summary>
        /// 啟用狀況
        /// </summary>
        public bool State { get; set; }
    }
}
