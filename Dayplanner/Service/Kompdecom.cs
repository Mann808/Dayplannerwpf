using Dayplanner.Class1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dayplanner.Service
{
    class Kompdecom
    {
        private readonly string PATH;
        public Kompdecom(string path)
        {
            PATH = path;
        }
        public BindingList<Model> Loaddata() 
        {
            var fileExist = File.Exists(PATH);
            if (!fileExist) 
            { 
                File.Create(PATH).Dispose();
                return new BindingList<Model>();
            }
            using (var reader = File.OpenText(PATH))
            {
                var filetext = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<Model>>(filetext);
            }
        }
        public void savedata(object Datalist)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(Datalist);
                writer.Write(output);
            }
        }
    }
}
