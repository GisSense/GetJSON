using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetJSON
{
    public class DataContextTest : INotifyPropertyChanged
    {
        private static DataContextTest instance;
        public event PropertyChangedEventHandler PropertyChanged;
        private string txtJson = "";

        private DataContextTest()
        {
        }

        public static DataContextTest Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataContextTest();
                }
                return instance;
            }
        }

        internal string TextJson
        {
            get
            {
                return this.txtJson;
            }
            set
            {
                this.txtJson = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TextJson"));
            }
        }
    }
}
