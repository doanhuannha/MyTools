using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace EncodeFile.Helpers
{
    public abstract class BaseWindow: Window
    {
        public BaseWindow()
        {
            
        }

        public bool EnableBindingProxy { get; set; }

        public new object DataContext
        {
            get => base.DataContext;
            set 
            {
                base.DataContext = value;
                Type type = base.DataContext.GetType();
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in properties)
                {
                    var proxy = new BindingProxy { Data = prop.GetValue(base.DataContext) };
                    this.Resources[prop.Name] = proxy;
                }
                

            }
        }
    }
}
