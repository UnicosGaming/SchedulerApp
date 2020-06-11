﻿using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchedulerApp.Models
{
    public abstract class ModelBase<T> : BindableBase
    {
        public string Id { get; }

        /// <summary>
        /// Default constructor generate it's own Id
        /// </summary>
        public ModelBase() : this(Guid.NewGuid().ToString()) { }

        /// <summary>
        /// Create an object with the specified Id
        /// </summary>
        /// <param name="id">GUID string</param>
        public ModelBase(string id) => Id = id;

        /// <summary>
        /// Create a clone of the model object
        /// </summary>
        /// <returns>Schedule object</returns>
        public virtual T Clone() => (T)this.MemberwiseClone();

        /// <summary>
        /// Copy the properties from the current object to the "target" object
        /// </summary>
        /// <param name="target">Model object to copy the values</param>
        /// <returns>T target with the values of the original object</returns>
        public abstract T CopyTo(T target);


    }
}