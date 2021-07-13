using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using Entities_Layer.Concrete;

namespace Data_Access_Layer.Functions
{
    /// <summary>
    /// Dosya klasör işlemlerinin yapıldığı bir classtır.
    /// </summary>
    public static class FileManager
    {
        /// <summary>
        /// Bu değişken uygulama verilerinin saklandığı dizinin yolunu tutar.
        /// </summary>
        public static readonly string APP_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Flappy Bird");

        /// <summary>
        /// Bu değişken skorların saklandığı dosyanın yolunu tutar.
        /// </summary>
        public static string SAVE_PATH = Path.Combine(APP_PATH, "scores.json");        

        /// <summary>
        /// Eğer uygulamanın verilerinin kaydedileceği klasör oluşturulmadıysa devre girecek fonksiyondur.
        /// </summary>
        public static void CreateAppDirIfNotExists()
        {
            if (Directory.Exists(APP_PATH)) return;

            Directory.CreateDirectory(APP_PATH);
        }

        /// <summary>
        /// Eğer uygulamanın verilerinin kaydedileceği dosya oluşturulmadıysa devreye girecek fonskiyondur.
        /// </summary>
        public static void CreateNotesFileIfNotExists()
        {
            if (File.Exists(SAVE_PATH)) return;

            CreateAppDirIfNotExists();

            var path = Path.GetDirectoryName(APP_PATH + "scores.json");
            Directory.CreateDirectory(path);
        }

        /// <summary>
        /// Verilen <see cref="Scores"/> listesini diske kaydeden fonskiyondur.
        /// </summary>
        /// <param name="scores">Kaydedilecek skorlar</param>
        public static void SaveData(List<Scores> scores)
        {
            CreateAppDirIfNotExists();

            using (StreamWriter file = new StreamWriter(SAVE_PATH))
            {
                file.WriteLine(JsonConvert.SerializeObject(scores, Formatting.Indented));

            }
        }
    }
}
