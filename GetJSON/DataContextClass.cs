using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetJSON
{
    public class DataContextClass : INotifyPropertyChanged
    {
        private static DataContextClass instance;
        public event PropertyChangedEventHandler PropertyChanged;
        private string txtJson = "";

        private DataContextClass()
        {
        }

        public static DataContextClass Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataContextClass();
                }
                return instance;
            }
        }

        public string TextJson
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
