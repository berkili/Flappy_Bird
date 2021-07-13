using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities_Layer.Concrete;

namespace Business_Layer.Functions
{
    /// <summary>
    /// Bu class leaderboard tablosuna veri gönderen listenin tüm işlemlerini yapar.
    /// </summary>
    public class LeadboardManagment
    {
        /// <summary>
        /// Bu liste <see cref="ScoreLeadboard"/> classında içerikleri alarak yeni bir liste oluşturur.
        /// </summary>
        public static List<ScoreLeadboard> ScoreLeadboards = new List<ScoreLeadboard>();

        /// <summary>
        /// Bu fonksiyon <see cref="DataManager.scores"/> listesindeki verilerin skorlarına göre ilk 10'nu yeni bir listeye kaydeder.
        /// </summary>
        /// <returns></returns>
        public static List<ScoreLeadboard> Leadboards()
        {
            int i = 1;
            foreach (var item in DataManager.scores.OrderByDescending(m => m.Score).Take(10))
            {
                ScoreLeadboard leadboard1 = new ScoreLeadboard();
                leadboard1.Rank = i;
                leadboard1.Name = item.Name;
                leadboard1.Score = item.Score;
                ScoreLeadboards.Add(leadboard1);
                i++;
            }
            return ScoreLeadboards;
        }
    }
}
