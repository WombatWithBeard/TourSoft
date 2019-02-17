using System;
using ToursSoft.Data.Interface;

namespace ToursSoft.Data.Models
{
    /// <summary>
    /// Base entity
    /// </summary>
    [Serializable]
    public class Model: IModel
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="id">Entity Id</param>
        public Model(Guid id)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }

        internal Model(){}

        /// <summary>
        /// Entity Id
        /// </summary>
        public Guid Id { get; set; }
    }
}