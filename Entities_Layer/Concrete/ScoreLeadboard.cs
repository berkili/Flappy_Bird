using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_Layer.Concrete
{
    /// <summary>
    /// Bu class leaderboard ekranında çıkan listenin içeriklerini barındırır.
    /// </summary>
    public class ScoreLeadboard
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }
    }
}
