using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CVS_ReadAndWrite12Jan21
{
    class ReadWrite_CSV
    {
        static void Main(string[] args)
        {
            List<string> UserID = new List<string>();
            String path = @"C:CSV Nesle\371a4c5ccb92.csv";
            string pathOut = @"C:CSV Nesle\New\371a4c5ccb92.csv";

            using (StreamReader sr = File.OpenText(path))
            {
                String s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    string[] values = s.Split(',');
                    UserID.Add(values[0]);
                }
            }

            var Counting = from u in UserID
                           group u by UserID into g
                           select new
                           {
                               cnt = g.Count()
                           };

            var DupliCount = from d in UserID
                             group d by d into c
                             let count = c.Count()
                             select new { Value = c.Key, Count = count };
         
            var DuplicOverThirty = from t in DupliCount
                                   where t.Count > 30
                                   select new { OverThirty = t };

            foreach (var o in DuplicOverThirty)
            {
                Console.WriteLine($"{o.OverThirty.Value}, {o.OverThirty.Count}"); 
                File.WriteAllText (pathOut, $"{o.OverThirty.Value}, {o.OverThirty.Count}\n");
                File.AppendAllText(pathOut, $"{o.OverThirty.Value}, {o.OverThirty.Count}\n");
            }
        }
    }
}