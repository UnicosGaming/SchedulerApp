using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public interface ICopiable<T>
    {
        /// <summary>
        /// Copy the properties from the current object to the "target" object
        /// </summary>
        /// <param name="target">Model object to copy the values</param>
        /// <returns>T target with the values of the original object</returns>
        T CopyTo(T target);
    }
}
