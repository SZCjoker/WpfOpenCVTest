using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using WpfOpenCVTest.BLL.DataCVHelper;
using WpfOpenCVTest.Dal;
using WpfOpenCVTest.Dal.Interface;
using WpfOpenCVTest.Dal.Models;
using WpfOpenCVTest.Dal.Repository;

namespace WpfOpenCVTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private readonly IRepository<JsonConfig> _repository;
        private readonly DatabaseContext _context;
        public MainWindow()
        {  
            InitializeComponent();
            _context = new DatabaseContext ();
            _repository = new GenericRepository<JsonConfig>(_context);
           LoadJson();
        }
        /// <summary>
        /// 二值化轉換
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Canny(object sender, RoutedEventArgs e)
        {
            var picData = new BitmapImage(new Uri(preImg.Source.ToString()));
            var maTfrom = picData.ToMat().Threshold(30,20,ThresholdTypes.Binary);
            Mat preData = maTfrom;           
            Mat afterData = new Mat();
            Cv2.Canny(preData,afterData,60,100);            
            afterImg.Source = afterData.ToBitmapSource();

        }
        /// <summary>
        /// 寫入資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Write(object sender, RoutedEventArgs e)
        {           
            var value = textInput.Text;
            var Jhelper = new JsonHelper();
            var afterData = Jhelper.CheckDataFormat(value) == true ?
                JsonConvert.DeserializeObject<JsonConfig>(value):null;
               
            var config = new JsonConfig { Name = afterData.Name, 
                                          Version = afterData.Version, 
                                          Cdate = afterData.Cdate,
                                          Udate = afterData.Udate,
                                          State = afterData.State};
            _repository.Create(config);
        }
        /// <summary>
        /// 讀取資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Load(object sender, RoutedEventArgs e)
        {   StringBuilder sb = new StringBuilder();
            var repository = new GenericRepository<JsonConfig>();
            try
            {
                var datas = _repository.GetAll().ToList();

                if (datas.Count() != 0)
                {
                    foreach (var data in datas)
                    {
                        sb.Append(JsonConvert.SerializeObject(data));

                        lab.Content = "";
                        lab.Content = sb.AppendLine();
                    }
                }
                else
                {
                    lab.Content = "資料庫無資料請新增資料";
                }
            }

            catch (Exception ex)
            {

                lab.Content =ex.Message;
            }
           
        }

        /// <summary>
        /// 預載
        /// </summary>
        public void LoadJson()
        {
            using (StreamReader r = new StreamReader("file.json"))
            {
                var datetime = DateTimeOffset.Now;
                var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
                
                string json = r.ReadToEnd();
                try
                {
                    List<JsonConfig> items = JsonConvert.DeserializeObject<List<JsonConfig>>(json);

                    var oldDatas = _repository.GetAll().ToList();
                    
                    if (oldDatas.Count() == 0)
                    {
                        foreach (var item in items)
                        {
                            _repository.Create(item);
                        }
                    }
                    
                    else
                    { 
                        foreach(var item in items)
                        {   
                            var newVercode = Convert.ToInt32(oldDatas.Last().Version.Last().ToString()) + 1;
                            var orignalCode = item.Version.Substring(0, item.Version.Length - 1);
                            item.Id = oldDatas.Last().Id+1;
                            item.Cdate = date.ToString();
                            item.Udate = date.ToString();
                            item.Version = orignalCode+(newVercode.ToString());
                            _repository.Create(item);
                        }
                    }

                }

                catch (Exception ex)
                {
                    lab.Content = ex.Message;
                }






            }
        }


       
    }
}
