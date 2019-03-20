using System;
using System.ComponentModel.DataAnnotations;
using ToursSoft.Data.Models.Interface;

namespace ToursSoft.Data
{
    /// <summary>
    /// Base entity
    /// </summary>
    [Serializable]
    public class Model: IModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Entity Id</param>
        protected Model(Guid id) //TODO: check for warnings
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }

        internal Model(){}

        /// <summary>
        /// Entity Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}