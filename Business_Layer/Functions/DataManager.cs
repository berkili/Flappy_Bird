using System;
using System.Collections.Generic;
using System.IO;
using Data_Access_Layer.Functions;
using Entities_Layer.Concrete;
using Newtonsoft.Json;

namespace Business_Layer.Functions
{
    public static class DataManager 
    {
        /// <summary>
        /// Oyundaki skorun ve username'i kaydeden bir field'dır.
        /// </summary>
        public static List<Scores> scores = new List<Scores>();

        /// <summary>
        /// Scores listesinin elemanlarını FileManager class'ı aracılığı ile json dosyasına kaydeden bir fonksiyon
        /// </summary>
        private static void SaveToDisk()
        {
            FileManager.SaveData(scores);
        }

        /// <summary>
        /// Bu fonksiyon hem score parametresini alıp ve Scores listesine ekleyip hemde kendinden önce yazılan bilgileri
        /// okuyan GetScoreList fonksiyonunu çağırıp sonra bunları dosyaya kaydetmek üzere SaveToDisk fonksiyonuna
        /// gönderir.
        /// </summary>
        /// <param name="score">Eklenecek scores objesi</param>
        public static void AddData(Scores score)
        {
            GetScoreList();
            scores.Add(score);
            SaveToDisk();            
        }

        /// <summary>
        /// Bu fonksiyon json dosyasında bulunan verileri çözerek, veriyi çekmemizi sağlar.
        /// </summary>
        /// <returns></returns>
        public static List<Scores> GetScoreList()
        {

            try
            {
                if (!File.Exists(FileManager.SAVE_PATH)) return null;
                return scores = JsonConvert.DeserializeObject<List<Scores>>(File.ReadAllText(FileManager.SAVE_PATH));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
