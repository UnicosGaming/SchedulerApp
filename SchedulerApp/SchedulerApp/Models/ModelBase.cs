using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public class ModelBase<T> : BindableBase
    {
        public virtual T Clone()
        {
            return (T)this.MemberwiseClone();
        }

    }
}
